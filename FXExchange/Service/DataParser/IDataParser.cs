using FXExchange.Model.CurrencyExchange;

namespace FXExchange.Service.DataParser;

/// <summary>
/// Responsible for reading data (currently values are read from exchangeRates.csv)
/// </summary>
public interface IDataParser
{
    /// <summary>
    /// Gets list of currency objects from source (currently hardcoded text file)
    /// </summary>
    /// <returns>List of currency objects</returns>
    IEnumerable<Currency> ParseCurrencies();
}