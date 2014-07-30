using System;
using System.Linq;

namespace Nextmethod.Cex
{
    public class OpenOrder : Order
    {


        public decimal Pending { get; internal set; }

        public TradeId Id { get; internal set; }

        public Timestamp Time { get; internal set; }


        public override string ToString()
        {
            return string.Format("Id: {0}, Type: {1}, Amount: {2}, Price: {3}, Pending: {4}, Time: {5}", Id, Type, Amount, Price, Pending, Time);
        }

        internal static OpenOrder FromDynamic(dynamic data)
        {
            return new OpenOrder
            {
                Amount = JsonHelpers.ToDecimal(data["amount"]),
                Id = data["id"],
                Pending = JsonHelpers.ToDecimal(data["pending"]),
                Price = JsonHelpers.ToDecimal(data["price"]),
                Time = data["time"],
                Type = Convert.ToString(data["type"]) == "sell" ? OrderType.Sell : OrderType.Buy
            };
        }

    }
}
