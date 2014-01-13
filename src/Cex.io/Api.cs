using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Nextmethod.Cex
{
    public sealed class Api
    {

        static Api()
        {
            if (HttpClientFactory.ConnectionLimit != null && HttpClientFactory.ConnectionLimit.Value != Constants.DefaultConnectionLimit)
            {
                ServicePointManager.DefaultConnectionLimit = HttpClientFactory.ConnectionLimit.Value;
            }
            else if (ServicePointManager.DefaultConnectionLimit == Constants.DefaultConnectionLimit)
            {
                ServicePointManager.DefaultConnectionLimit = Constants.DesiredConnectionLimit;
            }
        }


        public TimeSpan? Timeout { get; set; }

        private ApiCredentials Credentials { get; set; }


        public Api(string username, string apiKey, string apiSecret)
            : this(new ApiCredentials(username, apiKey, apiSecret)) {}

        public Api(ApiCredentials credentials)
        {
            Credentials = credentials;
        }


        public async Task<Ticker> Ticker(SymbolPair pair, CancellationTokenSource tokenSource = null)
        {
            const string basePath = "/ticker";
            var path = string.Format("{0}/{1}/{2}", basePath, pair.From, pair.To);

            return await GetServiceCall(
                path,
                Cex.Ticker.FromDynamic,
                tokenSource
                );
        }

        public async Task<OrderBook> OrderBook(SymbolPair pair, CancellationTokenSource tokenSource = null)
        {
            const string basePath = "/order_book";
            var path = string.Format("{0}/{1}/{2}", basePath, pair.From, pair.To);

            return await GetServiceCall(
                path,
                Cex.OrderBook.FromDynamic,
                tokenSource
                );
        }

        public async Task<IEnumerable<Trade>> TradeHistory(SymbolPair pair, CancellationTokenSource tokenSource = null)
        {
            const string basePath = "/trade_history";
            var path = string.Format("{0}/{1}/{2}", basePath, pair.From, pair.To);

            return await GetServiceCall(
                path,
                Trade.FromDynamic,
                tokenSource
                );
        }

        public async Task<Balance> AccountBalance(CancellationTokenSource tokenSource = null)
        {
            const string basePath = "/balance/";

            return await PostServiceCall(
                basePath,
                () => EmptyRequestParams,
                Balance.FromDynamic,
                tokenSource
                );
        }

        public async Task<IEnumerable<OpenOrder>> OpenOrders(SymbolPair pair, CancellationTokenSource tokenSource = null)
        {
            const string basePath = "/open_orders";
            var path = string.Format("{0}/{1}/{2}", basePath, pair.From, pair.To);

            return await PostServiceCall(
                path,
                () => EmptyRequestParams,
                x =>
                {
                    var ja = x as JsonArray;
                    return ja == null 
                        ? Enumerable.Empty<OpenOrder>() 
                        : ja.Select(OpenOrder.FromDynamic).ToArray();
                },
                tokenSource
                );
        }

        public async Task<OpenOrder> PlaceOrder(SymbolPair pair, Order order, CancellationTokenSource tokenSource = null)
        {
            const string basePath = "/place_order";
            var path = string.Format("{0}/{1}/{2}", basePath, pair.From, pair.To);

            return await PostServiceCall(
                path,
                () => new[]
                {
                    new KeyValuePair<string, string>("type", order.Type == OrderType.Sell ? "sell" : "buy"),
                    new KeyValuePair<string, string>("price", order.Price.ToString()),
                    new KeyValuePair<string, string>("amount", order.Amount.ToString())
                },
                OpenOrder.FromDynamic,
                tokenSource
                );
        }

        public async Task<bool> CancelOrder(TradeId tradeId, CancellationTokenSource tokenSource = null)
        {
            const string basePath = "/cancel_order/";
            

            return await PostServiceCall(
                basePath,
                () => new[] { new KeyValuePair<string, string>("id", tradeId.ToString()) },
                x => (bool) x,
                tokenSource
                );
        }

        #region Service Calls and Helpers


        [DebuggerHidden]
        private async Task<T> GetServiceCall<T>(string servicePath, Func<dynamic, T> resultFactory, CancellationTokenSource tokenSource = null)
        {
            tokenSource = tokenSource ?? new CancellationTokenSource();
            var uri = this.GetApiUri(servicePath);
            using (var client = this.NewHttpClient())
            {
                using (var response = await client.GetAsync(uri, tokenSource.Token))
                {
                    var body = await response.Content.ReadAsStringAsync();
                    dynamic json = SimpleJson.DeserializeObject(body);

                    ExceptionOracle.ThrowIfError(response, json);
                    return resultFactory(json);
                }
            }
        }

        [DebuggerHidden]
        private async Task<T> PostServiceCall<T>(string servicePath, Func<IEnumerable<KeyValuePair<string, string>>> paramFactory, Func<dynamic, T> resultFactory, CancellationTokenSource tokenSource = null)
        {
            tokenSource = tokenSource ?? new CancellationTokenSource();

            long nonce;
            var signature = Credentials.NewSignature(out nonce);
            var content = new FormUrlEncodedContent(
                new[]
                {
                    NewRequestParam(Constants.ApiParamKey, Credentials.ApiKey),
                    NewRequestParam(Constants.ApiParamSignature, signature),
                    NewRequestParam(Constants.ApiParamNonce, Convert.ToString(nonce))
                }
                    .Concat(paramFactory())
                );

            var uri = this.GetApiUri(servicePath);
            using (var client = this.NewHttpClient())
            {
                using (var response = await client.PostAsync(uri, content, tokenSource.Token))
                {
                    var body = await response.Content.ReadAsStringAsync();
                    dynamic json = SimpleJson.DeserializeObject(body);

                    ExceptionOracle.ThrowIfError(response, json);
                    return resultFactory(json);
                }
            }
        }

        private static KeyValuePair<string, string> NewRequestParam(string key, string value)
        {
            return new KeyValuePair<string, string>(key, value);
        }

        private static readonly IEnumerable<KeyValuePair<string, string>> EmptyRequestParams = Enumerable.Empty<KeyValuePair<string, string>>();

        #endregion


    }
}
