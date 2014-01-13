using System;
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
            _hmac = new HMACSHA256(EncodingHelpers.EncodeString(apiSecret));
        }

        public string Username { get; private set; }

        public string ApiKey { get; private set; }


        public string NewSignature(out long nonce)
        {
            var n = Nonce.Next;
            var bytes = EncodingHelpers.EncodeString(string.Format("{0}{1}{2}", n, Username, ApiKey));
            var hash = _hmac.ComputeHash(bytes);

            nonce = n;
            // Hexencode hash
            return string.Concat(hash.Select(b => b.ToString("X2")));
        }


    }
}
