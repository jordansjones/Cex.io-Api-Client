using System;
using System.Linq;
using System.Text;

namespace Nextmethod.Cex
{
    public static class EncodingHelpers
    {

        private static readonly Encoding StringEncoder = Encoding.UTF8;

        public static byte[] EncodeString(string value)
        {
            value = value ?? String.Empty;
            return StringEncoder.GetBytes(value);
        }

        public static string DecodeBytes(byte[] value)
        {
            return StringEncoder.GetString(value);
        }

    }
}
