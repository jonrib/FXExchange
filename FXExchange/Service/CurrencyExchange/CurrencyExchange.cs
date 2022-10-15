using FXExchange.Helper;
using FXExchange.Model.Exceptions;
using FXExchange.Service.CurrencyLoader;

namespace FXExchange.Service.CurrencyExchange;

public class CurrencyExchange: ICurrencyExchange
{
    private IDictionary<string, decimal> _currencies;

    public CurrencyExchange(ICurrencyLoader currencyLoader)
    {
        _currencies = currencyLoader.LoadCurrencies();
    }

    public decimal ExchangeCurrencies(string exchange, decimal amount)
    {
        exchange = exchange.ToUpper();

        if (!amount.ValidateExchangeAmount())
        {
            throw new InvalidExchangeAmountException($"{amount} is not a valid amount to exchange");
        }

        if (_currencies.ContainsKey(exchange))
        {
            return _currencies[exchange] * amount;
        }

        throw new ExchangeRateUnknownException($"Exchange rate {exchange} is unknown");
    }
}