using System;
using System.Linq;
using System.Threading;

namespace Nextmethod.Cex
{
    public static class Nonce
    {

        private const long IncrementAmount = 1L;

        private static long _nonce;

        static Nonce()
        {
            Interlocked.Exchange(ref _nonce, Convert.ToInt64(Helpers.UtcDateTimeToTimestamp()));
        }


        public static long Next
        {
            get
            {
                return Interlocked.Add(ref _nonce, IncrementAmount);
            }
        }


    }
}
