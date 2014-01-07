using System;
using System.Linq;
using System.Net.Http;

namespace Nextmethod.Cex
{
    public class CexNonceException : CexApiException
    {

        public CexNonceException(HttpResponseMessage response, string message) : base(response, message) {}

    }
}
