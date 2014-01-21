using System;
using System.Linq;
using System.Net.Http;

namespace Nextmethod.Cex
{
    public class CexInvalidApiKeyException : CexApiException
    {

        public CexInvalidApiKeyException(HttpResponseMessage response, string message) : base(response, message) {}

    }
}
