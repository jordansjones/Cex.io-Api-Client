using System;
using System.Linq;
using System.Threading;

namespace Nextmethod.Cex
{
    public static class Nonce
    {

        private static long _nonce;

        static Nonce()
        {
            Interlocked.Exchange(ref _nonce, Helpers.UtcDateTimeToTimestamp().ToLong());
        }


        public static long Next
        {
            get
            {
                return Interlocked.Add(ref _nonce, 1L);
            }
        }


    }
}
