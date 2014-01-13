using System;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace Nextmethod.Cex
{
    public static class Nonce
    {
        private const long NonceEpoch = 635241312000000000L; // January 01, 2014 in ticks

        private const int IncrementAmount = 1;

        private static int _nonce;

        private static readonly object Locker = new object();

        static Nonce()
        {
            lock (Locker)
            {
                var nonceString = Convert.ToInt32((DateTime.UtcNow - new DateTime(NonceEpoch, DateTimeKind.Utc)).TotalSeconds).ToString(CultureInfo.InvariantCulture).PadRight(9, '0');
                _nonce = int.Parse(nonceString);
            }
        }

        public static int Next
        {
            get
            {
                return Interlocked.Add(ref _nonce, IncrementAmount);
            }
        }


    }
}
