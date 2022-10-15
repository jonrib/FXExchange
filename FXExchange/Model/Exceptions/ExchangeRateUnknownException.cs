namespace FXExchange.Model.Exceptions;

/// <summary>
/// Exception that is thrown when the exchange rate is unknown
/// </summary>
public class ExchangeRateUnknownException : Exception
{
    public ExchangeRateUnknownException(string message) : base(message) { }
}