using System;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Nextmethod.Cex
{
    public class CexApiException : Exception
    {

        public CexApiException(HttpResponseMessage response, string message) : base(message)
        {
            var request = response.RequestMessage;
            RequestMethod = request.Method;
            RequestUri = request.RequestUri;

            ResponseStatusCode = response.StatusCode;
            ResponseReasonPhrase = response.ReasonPhrase;
        }

        public HttpMethod RequestMethod { get; private set; }

        public Uri RequestUri { get; private set; }

        public HttpStatusCode ResponseStatusCode { get; private set; }

        public string ResponseReasonPhrase { get; private set; }

        public override string ToString()
        {
            return string.Format("{0}, RequestMethod: {1}, RequestUri: {2}, ResponseStatusCode: {3}, ResponseReasonPhrase: {4}", base.ToString(), RequestMethod, RequestUri, ResponseStatusCode, ResponseReasonPhrase);
        }

    }
}
