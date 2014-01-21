using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Nextmethod.Cex
{
    public class GhashApi : ICexClient
    {

        #region Static Helpers

        private static readonly Func<IEnumerable<KeyValuePair<string, string>>> EmptyRequestParams = () => Enumerable.Empty<KeyValuePair<string, string>>();

        static GhashApi()
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

        public GhashApi(string username, string apiKey, string apiSecret)
            : this(new ApiCredentials(username, apiKey, apiSecret)) {}

        public GhashApi(ApiCredentials credentials = null)
        {
            Credentials = credentials;
        }

        public ApiCredentials Credentials { get; set; }

        public Func<Uri> BasePathFactory { get { return ApiUriFactory.GetGHash; } }

        public TimeSpan? Timeout { get; set; }


        public async Task<Hashrate> Hashrate(CancellationToken? cancelToken = null)
        {
            const string basePath = "/hashrate";

            try
            {
                return await this.PostToService(
                    basePath,
                    EmptyRequestParams,
                    Cex.Hashrate.FromDynamic,
                    cancelToken
                    );
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }
        }

        public async Task<IEnumerable<KeyValuePair<string, WorkerHashrate>>> WorkersHashRate(CancellationToken? cancelToken = null)
        {
            const string basePath = "/workers";

            try
            {
                return await this.PostToService(
                    basePath,
                    EmptyRequestParams,
                    x =>
                    {
                        var jo = x as JsonObject;
                        return jo == null
                            ? Enumerable.Empty<KeyValuePair<string, WorkerHashrate>>()
                            : jo.ToDictionary(
                            o => o.Key,
                            o => WorkerHashrate.FromDynamic(o.Value)
                            );
                    },
                    cancelToken);
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }
        }

    }
}