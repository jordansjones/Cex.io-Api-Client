using System;
using System.Linq;
using System.Net.Http;

namespace Nextmethod.Cex
{
    internal static class ExceptionOracle
    {

        public static void ThrowIfError(HttpResponseMessage response, dynamic json)
        {
            if (json == null) return;

            if (!Helpers.ContainsProperty(json, Constants.ErrorProperty)) return;

            var error = json[Constants.ErrorProperty];
            if (string.IsNullOrWhiteSpace(error)) return;

            CexApiException exception;

            if (error == Constants.NonceMustBeIncremented)
            {
                exception = new CexNonceException(response, Constants.NonceMustBeIncremented);
            }
            else
            {
                exception = new CexApiException(response, error);
            }

            throw exception;
        }

    }
}
