# Cex.io Api Client

[Cex.IO](https://cex.io/r/0/jordansjones/0/) API Client Library for C# 4.5

Donations always welcome: **1KLetr9U8DCVjJK1czT7KB4PxpyLhQHHJF**

## Example

```cs

public class Program
{
    public Api Client { get; private set; }

    public async Task CexExample()
    {
        const string CexUsername = ""; // Your username on Cex.io
        const string CexApiKey = ""; // Your API key
        const string CexApiSecret = ""; // Your API secret

        // Using the ApiCredentials Class
        // Client = new Api(new ApiCredentials(CexUsername, CexApiKey, CexApiSecret));
        // Or not
        Client = new Api(CexUsername, CexApiKey, CexApiSecret);

        // Get Ticker Data
        var tickerData = await Client.Ticker(SymbolPair.GHS_BTC);

        // Get Order Book
        var orderBook = await Client.OrderBook(SymbolPair.GHS_BTC);

        // Get Trade history
        var tradeHistory = await Client.TradeHistory(SymbolPair.GHS_BTC);

        // Get Account Balance
        var accountBalance = await Client.AccountBalance();

        // Get Open Orders
        var openOrders = await Client.OpenOrders(SymbolPair.GHS_BTC);

        // Place an order
        var orderData = await Client.PlaceOrder(
            SymbolPair.GHS_BTC,
            new Order
            {
                Amount = 1.00m,
                Price = 0.04644000m,
                Type = OrderType.Buy
            });

        // Cancel an order
        var didSucceed = await Client.CancelOrder(orderData.Id);
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