namespace FXExchange.Service.CurrencyExchange;

/// <summary>
/// Responsible for providing exchange amounts
/// </summary>
public interface ICurrencyExchange
{
    /// <summary>
    /// Exchanges currencies
    /// </summary>
    /// <param name="exchange">ISO1/ISO2 where ISO1 is currency being exchanged and ISO2 is currency being exchanged to</param>
    /// <param name="amount">amount to exchange</param>
    /// <returns>exchanged amount</returns>
    decimal ExchangeCurrencies(string exchange, decimal amount);
}