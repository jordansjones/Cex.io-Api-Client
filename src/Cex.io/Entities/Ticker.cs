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
                Ask = JsonHelpers.ToDecimal(data["ask"]),
                Bid = JsonHelpers.ToDecimal(data["bid"]),
                High = JsonHelpers.ToDecimal(data["high"]),
                Last = JsonHelpers.ToDecimal(data["last"]),
                Low = JsonHelpers.ToDecimal(data["low"]),
                Volume = JsonHelpers.ToDecimal(data["volume"]),
                Timestamp = data["timestamp"]
            };
        }

        public override string ToString()
        {
            return string.Format("Ask: {0}, Bid: {1}, High: {2}, Low: {3}, Last: {4}, Volume: {5}, Timestamp: {6}", Ask, Bid, High, Low, Last, Volume, Timestamp);
        }

    }
}
