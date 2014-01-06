using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Nextmethod.Cex
{
    public sealed class Api
    {

        private const int DefaultConnectionLimit = 2;

        private const int DesiredConnectionLimit = 100;

        static Api()
        {
            if (ServicePointManager.DefaultConnectionLimit == DefaultConnectionLimit)
                ServicePointManager.DefaultConnectionLimit = DesiredConnectionLimit;
        }

        public Api(string username, string apiKey, string apiSecret)
            : this(new ApiCredentials(username, apiKey, apiSecret)) {}

        public Api(ApiCredentials credentials)
        {
            Credentials = credentials;
        }

        public TimeSpan? Timeout { get; set; }

        private ApiCredentials Credentials { get; set; }


        public async Task<Ticker> GetTicker(SymbolPair pair, CancellationTokenSource tokenSource = null)
        {
            const string basePath = "/ticker";

            tokenSource = tokenSource ?? new CancellationTokenSource();
            var path = string.Format("{0}/{1}/{2}", basePath, pair.From, pair.To);
            var uri = this.GetApiUri(path);

            using (var client = this.NewHttpClient())
            {
                using (var response = await client.GetAsync(uri, tokenSource.Token))
                {
                    var body = await response.Content.ReadAsStringAsync();
                    dynamic json = SimpleJson.DeserializeObject(body);
                    return Ticker.FromDynamic(json);
                }
            }
        }


    }
}
