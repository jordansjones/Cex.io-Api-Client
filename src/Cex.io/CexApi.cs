using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Nextmethod.Cex
{
    public sealed class CexApi : ICexClient
    {

        #region Static Helpers

        private static readonly Func<IEnumerable<KeyValuePair<string, string>>> EmptyRequestParams = () => Enumerable.Empty<KeyValuePair<string, string>>();

        static CexApi()
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

        #endregion


        public CexApi(string username, string apiKey, string apiSecret)
            : this(new ApiCredentials(username, apiKey, apiSecret)) {}

        public CexApi(ApiCredentials credentials = null)
        {
            Credentials = credentials;
        }

        public ApiCredentials Credentials { get; set; }

        public Func<Uri> BasePathFactory { get { return ApiUriFactory.GetCex; } }

        public TimeSpan? Timeout { get; set; }


        public async Task<Ticker> Ticker(SymbolPair pair, CancellationToken? cancelToken = null)
        {
            const string basePath = "/ticker";
            var path = string.Format("{0}/{1}/{2}", basePath, pair.From, pair.To);

            try
            {
                return await this.GetFromService(
                    path,
                    json => Cex.Ticker.FromDynamic(pair, json),
                    cancelToken
                    );
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }
        }

        public async Task<OrderBook> OrderBook(SymbolPair pair, CancellationToken? cancelToken = null)
        {
            const string basePath = "/order_book";
            var path = string.Format("{0}/{1}/{2}", basePath, pair.From, pair.To);

            try
            {
                return await this.GetFromService(
                    path,
                    Cex.OrderBook.FromDynamic,
                    cancelToken
                    );
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }
        }

        public async Task<IEnumerable<Trade>> TradeHistory(SymbolPair pair, TradeId? tradeId = null, CancellationToken? cancelToken = null)
        {
            const string basePath = "/trade_history";
            var path = string.Format("{0}/{1}/{2}", basePath, pair.From, pair.To);
            if (tradeId != null)
                path += string.Format("/?since={0}", tradeId.Value);

            try
            {
                return await this.GetFromService(
                    path,
                    Trade.FromDynamic,
                    cancelToken
                    );
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }
        }

        public async Task<Balance> AccountBalance(CancellationToken? cancelToken = null)
        {
            const string basePath = "/balance/";

            try
            {
                return await this.PostToService(
                    basePath,
                    EmptyRequestParams,
                    Balance.FromDynamic,
                    cancelToken
                    );
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }
        }

        public async Task<IEnumerable<OpenOrder>> OpenOrders(SymbolPair pair, CancellationToken? cancelToken = null)
        {
            const string basePath = "/open_orders";
            var path = string.Format("{0}/{1}/{2}", basePath, pair.From, pair.To);

            try
            {
                return await this.PostToService(
                    path,
                    EmptyRequestParams,
                    x =>
                    {
                        var ja = x as JsonArray;
                        return ja == null
                            ? Enumerable.Empty<OpenOrder>()
                            : ja.Select(OpenOrder.FromDynamic).ToArray();
                    },
                    cancelToken
                    );
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }
        }

        public async Task<OpenOrder> PlaceOrder(SymbolPair pair, Order order, CancellationToken? cancelToken = null)
        {
            const string basePath = "/place_order";
            var path = string.Format("{0}/{1}/{2}", basePath, pair.From, pair.To);

            try
            {
                return await this.PostToService(
                    path,
                    () => new[]
                    {
                        this.NewRequestParam("type", order.Type == OrderType.Sell ? "sell" : "buy"),
                        this.NewRequestParam("price", order.Price),
                        this.NewRequestParam("amount", order.Amount)
                    },
                    OpenOrder.FromDynamic,
                    cancelToken
                    );
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }
        }

        public async Task<bool> CancelOrder(TradeId tradeId, CancellationToken? cancelToken = null)
        {
            const string basePath = "/cancel_order/";

            try
            {
                return await this.PostToService(
                    basePath,
                    () => new[] {this.NewRequestParam("id", tradeId)},
                    x => (bool) x,
                    cancelToken
                    );
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }
        }


    }
}
