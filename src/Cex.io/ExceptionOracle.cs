using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;

namespace Nextmethod.Cex
{
    internal static class ExceptionOracle
    {
        
        [DebuggerHidden]
        public static void ThrowIfError(HttpResponseMessage response, dynamic json)
        {
            if (json == null) return;

            if (!JsonHelpers.ContainsProperty(json, Constants.ErrorProperty)) return;

            var error = json[Constants.ErrorProperty];
            if (string.IsNullOrWhiteSpace(error)) return;

            CexApiException exception;

            if (error == Constants.NonceMustBeIncremented)
            {
                exception = new CexNonceException(response, Constants.NonceMustBeIncremented);
            }
            else if (error == Constants.PermissionDenied)
            {
                exception = new CexPermissionDeniedException(response, Constants.PermissionDenied);
            }
            else
            {
                exception = new CexApiException(response, error);
            }

            throw exception;
        }

    }
}
