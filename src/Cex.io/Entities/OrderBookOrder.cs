using System;
using System.Linq;

namespace Nextmethod.Cex
{
    public struct OrderBookOrder
    {

        private readonly decimal _amount;

        private readonly decimal _price;

        public OrderBookOrder(decimal price, decimal amount)
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

        public override string ToString()
        {
            return String.Format("Price: {0}, Amount: {1}", Price, Amount);
        }

    }
}