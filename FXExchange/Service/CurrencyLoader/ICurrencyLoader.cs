namespace FXExchange.Service.CurrencyLoader;

/// <summary>
/// Responsible for loading Dictionary with needed currency exchange rates
/// </summary>
public interface ICurrencyLoader
{
    /// <summary>
    /// method which loads a dictionary with all the known exchange rates
    /// </summary>
    /// <returns>Dictionary where key is ISO/ISO and value is exchange rate</returns>
    IDictionary<string, decimal> LoadCurrencies();
}