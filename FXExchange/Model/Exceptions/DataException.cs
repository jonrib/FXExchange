namespace FXExchange.Model.Exceptions;

/// <summary>
/// Exception that is thrown when data in file is incorrect
/// </summary>
public class DataException: Exception
{
    public DataException(string message): base(message) { }
}