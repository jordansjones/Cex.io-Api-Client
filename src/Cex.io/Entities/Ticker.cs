using System;
using System.Linq;

namespace Nextmethod.Cex
{
    public class Ticker
    {

        public SymbolPair SymbolPair { get; internal set; }

        public decimal Ask { get; internal set; }

        public decimal Bid { get; internal set; }

        public decimal High { get; internal set; }

        public decimal Last { get; internal set; }

        public decimal Low { get; internal set; }

        public decimal Volume { get; internal set; }

        public Timestamp Timestamp { get; internal set; }


        internal static Ticker FromDynamic(SymbolPair symbolPair, dynamic data)
        {
            return new Ticker
            {
                SymbolPair = symbolPair,
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
            return string.Format("SymbolPair: {0}, Ask: {1}, Bid: {2}, High: {3}, Last: {4}, Low: {5}, Volume: {6}, Timestamp: {7}", SymbolPair, Ask, Bid, High, Last, Low, Volume, Timestamp);
        }

    }
}
