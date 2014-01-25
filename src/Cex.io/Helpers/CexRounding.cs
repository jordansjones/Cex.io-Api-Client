using System;
using System.Linq;

namespace Nextmethod.Cex
{
    public static class CexRounding
    {

        private const decimal CexRoundingFactor = 100000000M;

        public static decimal CexRound(this decimal This)
        {
            return (Math.Ceiling(This * CexRoundingFactor)) / CexRoundingFactor;
        }


    }
}
