using System;
using System.Linq;

namespace Nextmethod.Cex
{

    public class Order
    {

        public decimal Amount { get; set; }

        public TradeId Id { get; internal set; }

        public decimal Pending { get; set; }

        public decimal Price { get; set; }

        public Timestamp Time { get; set; }

        public OrderType Type { get; set; }

    }
}
