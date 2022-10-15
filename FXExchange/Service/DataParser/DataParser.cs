using FXExchange.Helper;
using FXExchange.Model.CurrencyExchange;
using FXExchange.Model.Exceptions;
using Microsoft.Extensions.Logging;

namespace FXExchange.Service.DataParser;

public class DataParser : IDataParser
{
    private readonly ILogger<DataParser> _logger;
    private const string ExchangeRateSourceFile = $"../../../ExchangeRatesInDKK.csv";
    private const char FileSeparator = ';';

    public DataParser(ILogger<DataParser> logger)
    {
        _logger = logger;
    }

    public IEnumerable<Currency> ParseCurrencies()
    {
        var result = new List<Currency>();

        try
        {
            using var reader = new StreamReader(ExchangeRateSourceFile);

            var isosLoaded = new HashSet<string>();
            var line = reader.ReadLine();

            while (line != null)
            {
                var currency = ConvertStringLineToCurrency(line);

                if (currency != null)
                {
                    if (isosLoaded.Contains(currency.ISO))
                    {
                        throw new Exception($"Incorrect data in file {ExchangeRateSourceFile}. Multiple amounts for one ISO detected.");
                    }

                    result.Add(currency);
                    isosLoaded.Add(currency.ISO);
                }
                
                line = reader.ReadLine();
            }
        }
        catch (Exception e)
        {
            //TODO not covered by tests
            _logger.LogCritical(e, $"Failed to read currency data from file: {ExchangeRateSourceFile}");
        }

        return result;
    }

    /// <summary>
    /// Converts CSV line with format (Currency name, ISO, Amount) to Currency object
    /// </summary>
    /// <param name="line"></param>
    /// <returns></returns>
    private Currency ConvertStringLineToCurrency(string line)
    {
        try
        {
            var split = line.Split(FileSeparator);

            if (split[1].ValidateISO())
            {
                return new Currency
                {
                    Name = split[0],
                    ISO = split[1],
                    Amount = decimal.Parse(split[2])
                };
            }
            else
            {
                throw new DataException($"String {split[1]} is not a valid ISO");
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Failed to convert line: {line} to currency object");
        }

        return null;
    }
}