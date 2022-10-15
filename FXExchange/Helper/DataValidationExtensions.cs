namespace FXExchange.Helper;

public static class DataValidationExtensions
{
    /// <summary>
    /// method for validating ISO
    /// </summary>
    /// <param name="ISO"></param>
    public static bool ValidateISO(this string ISO)
    {
        return ISO is { Length: 3 } && ISO.ToUpper() == ISO;
    }

    /// <summary>
    /// Method for validating amount being exchanged
    /// </summary>
    /// <param name="amount">Amount being exchanged</param>
    /// <returns></returns>
    public static bool ValidateExchangeAmount(this decimal amount)
    {
        return amount > 0;
    }
}