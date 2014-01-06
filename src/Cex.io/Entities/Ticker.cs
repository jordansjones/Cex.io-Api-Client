using System;
using System.Linq;

namespace Nextmethod.Cex
{
    public class Ticker
    {

        public decimal Ask { get; internal set; }

        public decimal Bid { get; internal set; }

        public decimal High { get; internal set; }

        public decimal Last { get; internal set; }

        public decimal Low { get; internal set; }

        public decimal Volume { get; internal set; }

        public Timestamp Timestamp { get; internal set; }


        internal static Ticker FromDynamic(dynamic data)
        {
            return new Ticker
            {
                Ask = decimal.Parse(data["ask"]),
                Bid = decimal.Parse(data["bid"]),
                High = decimal.Parse(data["high"]),
                Last = decimal.Parse(data["last"]),
                Low = decimal.Parse(data["low"]),
                Volume = decimal.Parse(data["volume"]),
                Timestamp = data["timestamp"]
            };
        }
    }
}
