# Cex.io Api Client

[Cex.IO](https://cex.io/?c=4&a=1100&o=2&s=sc) API Client Library for C# 4.5

Donations always welcome: **1KLetr9U8DCVjJK1czT7KB4PxpyLhQHHJF**

## Example

```cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Nextmethod.Cex;

namespace SimpleExample
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().RunExample().Wait();
        }

        const string CexUsername = ""; // Your username on Cex.io
        const string CexApiKey = ""; // Your API key
        const string CexApiSecret = ""; // Your API secret

        private CexApi CexClient { get; set; }

        private GhashApi GhashClient { get; set; }

        private async Task RunExample()
        {

            // Using the ApiCredentials Class
            // Client = new Api(new ApiCredentials(CexUsername, CexApiKey, CexApiSecret));
            // Or not
            // CexClient = new CexApi(CexUsername, CexApiKey, CexApiSecret);
            // ApiCredentials/(Username, ApiKey, ApiSecret) not needed for public functions
            CexClient = new CexApi();

            // Get Ticker Data
            Ticker tickerData = await CexClient.Ticker(SymbolPair.GHS_BTC);

            // Get Order Book
            OrderBook orderBook = await CexClient.OrderBook(SymbolPair.GHS_BTC);

            // Get Trade history
            IEnumerable<Trade> tradeHistory = await CexClient.TradeHistory(SymbolPair.NMC_BTC);

            // ApiCredentials required for user specific calls or "Private Functions"
            CexClient = new CexApi(CexUsername, CexApiKey, CexApiSecret);

            // Get Account Balance
            Balance accountBalance = await CexClient.AccountBalance();

            // Get Open Orders
            IEnumerable<OpenOrder> openOrders = await CexClient.OpenOrders(SymbolPair.LTC_BTC);

            // Place an order
            OpenOrder openOrder = await CexClient.PlaceOrder(
                SymbolPair.GHS_BTC,
                new Order
                {
                    Amount = 1.00m,
                    Price = 0.04644000m,
                    Type = OrderType.Buy
                });

            // Cancel an order
            bool didSucceed = await CexClient.CancelOrder(openOrder.Id);

            // GHash.IO Example
            GhashClient = new GhashApi(new ApiCredentials(CexUsername, CexApiKey, CexApiSecret));

            // Get Hash Rate
            Hashrate hashrate = await GhashClient.Hashrate();

            // Get Workers Hash Rate
            IEnumerable<KeyValuePair<string, WorkerHashrate>> workerHashRate = await GhashClient.WorkersHashRate();
        }
    }
}

```

## License

> The MIT License (MIT)
> 
> Copyright (c) 2014 Jordan S. Jones
> 
> Permission is hereby granted, free of charge, to any person obtaining a copy of
> this software and associated documentation files (the "Software"), to deal in
> the Software without restriction, including without limitation the rights to
> use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
> the Software, and to permit persons to whom the Software is furnished to do so,
> subject to the following conditions:
> 
> The above copyright notice and this permission notice shall be included in all
> copies or substantial portions of the Software.
> 
> THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
> IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
> FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
> COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
> IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
> CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
