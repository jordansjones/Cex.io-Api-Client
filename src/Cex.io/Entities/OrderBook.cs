using System;
using System.Collections.Generic;
using System.Linq;

namespace Nextmethod.Cex
{
    public class OrderBook
    {

        public IEnumerable<Order> Asks { get; internal set; }

        public IEnumerable<Order> Bids { get; internal set; }

        public Timestamp Timestamp { get; private set; }

        public struct Order
        {

            private readonly decimal _amount;

            private readonly decimal _price;

            public Order(decimal price, decimal amount)
            {
                _price = price;
                _amount = amount;
            }

            public decimal Price
            {
                get { return _price; }
            }

            public decimal Amount
            {
                get { return _amount; }
            }

        }

    }
}
