using System;
using System.Collections.Generic;
using System.Linq;

namespace Nextmethod.Cex
{

    public class Trade
    {

        public decimal Amount { get; internal set; }

        public Timestamp Date { get; internal set; }

        public decimal Price { get; internal set; }

        public TradeId Id { get; internal set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, Price: {1}, Amount: {2}, Date: {3}", Id, Price, Amount, Date);
        }


        internal static IEnumerable<Trade> FromDynamic(dynamic data)
        {
            return ((IEnumerable<dynamic>) data).Select(
                x => new Trade
                {
                    Amount = decimal.Parse(x["amount"]),
                    Date = x["date"],
                    Price = decimal.Parse(x["price"]),
                    Id = x["tid"]
                });
        }

    }
}
