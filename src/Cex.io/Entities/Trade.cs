using System;
using System.Linq;

namespace Nextmethod.Cex
{

    public class Trade
    {

        public decimal Amount { get; internal set; }

        public Timestamp Date { get; internal set; }

        public decimal Price { get; internal set; }

        public TradeId Id { get; internal set; }

    }
}
