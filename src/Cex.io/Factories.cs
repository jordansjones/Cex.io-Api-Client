using System;
using System.Linq;
using System.Net.Http;

namespace Nextmethod.Cex
{
    public static class HttpClientFactory
    {

        public static Func<HttpClient> Get { get; set; }

        static HttpClientFactory()
        {
            Get = () => new HttpClient();
        }

        internal static HttpClient NewHttpClient(this Api This)
        {
            var client = Get();
            client.MaxResponseContentBufferSize = int.MaxValue;

            if (This.Timeout != null)
            {
                client.Timeout = This.Timeout.Value;
            }

            return client;
        }

    }

    public static class ApiUriFactory
    {

        public const string BaseApiUri = "https://cex.io/api";

        public static Func<Uri> Get { get; set; }

        static ApiUriFactory()
        {
            Get = () => new Uri(BaseApiUri);
        }

        internal static Uri GetApiUri(this Api This, string path)
        {
            var baseUri = Get().AbsoluteUri;
            if (!baseUri.EndsWith("/"))
                baseUri += "/";

            if (path.StartsWith("/"))
                path = path.Substring(1);

            return new Uri(string.Concat(baseUri, path));
        }

    }
}
