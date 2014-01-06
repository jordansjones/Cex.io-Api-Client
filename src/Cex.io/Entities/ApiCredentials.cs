using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography;

namespace Nextmethod.Cex
{
    public sealed class ApiCredentials
    {

        private readonly HMAC _hmac;

        public ApiCredentials(string username, string apiKey, string apiSecret)
        {
            Username = username;
            ApiKey = apiKey;
            _hmac = new HMACSHA256(Helpers.EncodeString(apiSecret));
        }

        public string Username { get; private set; }

        public string ApiKey { get; private set; }

        [Pure]
        public string NewSignature(long nonce)
        {
            var bytes = Helpers.EncodeString(string.Format("{0}{1}{2}", nonce, Username, ApiKey));
            var hash = _hmac.ComputeHash(bytes);
            // Hexencode hash
            return string.Concat(hash.Select(b => b.ToString("X2")));
        }


    }
}
