using FXExchange.Model.Exceptions;
using FXExchange.Service.CurrencyExchange;
using FXExchange.Service.CurrencyLoader;
using FXExchange.Service.DataParser;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var ExchangeKwd = "Exchange";

//TODO possibly refactor and move to separate class
//TODO errors are currently logged to console...
var serviceProvider = new ServiceCollection()
    .AddLogging(configure => configure.AddConsole())
    .AddSingleton<ICurrencyExchange, CurrencyExchange>()
    .AddSingleton<ICurrencyLoader, CurrencyLoader>()
    .AddSingleton<IDataParser, DataParser>()
    .BuildServiceProvider();

var currencyExchangeService = serviceProvider.GetService<ICurrencyExchange>();

Console.WriteLine("Exchange\nUsage: Exchange <currency pair> <amount to exchange>");

//TODO probably not the cleanest way to read input...
string line;
do
{
    line = Console.ReadLine();

    if (!string.IsNullOrEmpty(line))
    {
        var arguments = line.Split(' ');

        //TODO this logic is not covered by tests and could be moved to separate class...
        if (arguments.Length == 3 && arguments[0] == ExchangeKwd && decimal.TryParse(arguments[2], out var amount))
        {
            try
            {
                Console.WriteLine(currencyExchangeService?.ExchangeCurrencies(arguments[1], amount));
            }
            catch (InvalidExchangeAmountException)
            {
                Console.WriteLine($"{amount} is not a valid amount to exchange");
            }
            catch (ExchangeRateUnknownException)
            {
                Console.WriteLine($"{arguments[1]} exchange rate is unknown");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unhandled exception: {e.Message}");
            }
        }
    }

} while (!string.IsNullOrEmpty(line));
