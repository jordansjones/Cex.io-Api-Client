using System;
using System.Linq;

namespace Nextmethod.Cex
{
    public static class CexRounding
    {

        private const decimal CexRoundingFactor = 100000000M;

        public static decimal CexRound(this decimal This)
        {
            if (This == 0.0M) return This;
            return (Math.Ceiling(This * CexRoundingFactor)) / CexRoundingFactor;
        }


    }
}
