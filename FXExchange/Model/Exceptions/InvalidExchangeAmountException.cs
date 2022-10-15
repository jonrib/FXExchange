namespace FXExchange.Model.Exceptions;

/// <summary>
/// Exception that is thrown when exchange amount is invalid
/// </summary>
public class InvalidExchangeAmountException : Exception
{
    public InvalidExchangeAmountException(string message) : base(message) { }
}