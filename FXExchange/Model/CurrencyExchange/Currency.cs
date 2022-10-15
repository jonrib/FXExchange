namespace FXExchange.Model.CurrencyExchange;

/// <summary>
/// This model describes the data given in the task currency name, ISO and amount of DDK needed to purchase 100 of it
/// </summary>
public class Currency
{
    public string Name { get; set; }
    public string ISO { get; set; }
    public decimal Amount { get; set; }
}
