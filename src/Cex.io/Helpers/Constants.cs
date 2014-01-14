using System;
using System.Linq;

namespace Nextmethod.Cex
{
    internal static class Constants
    {

        #region Api Related

        internal const string BaseApiUri = "https://cex.io/api";

        internal const string GHashBaseApiUri = BaseApiUri + "/ghash.io";

        internal const string ApiParamAmount = "amount";

        internal const string ApiParamKey = "key";

        internal const string ApiParamNonce = "nonce";

        internal const string ApiParamPrice = "price";

        internal const string ApiParamSignature = "signature";

        internal const string ApiParamType = "type";

        #endregion



        #region Exception Related

        internal const string ErrorProperty = "error";

        internal const string NonceMustBeIncremented = "Nonce must be incremented";

        internal const string PermissionDenied = "Permission denied";

        #endregion


        #region HttpClient Related

        internal const int DefaultConnectionLimit = 2;

        internal const int DesiredConnectionLimit = 100;

        #endregion
    }
}
