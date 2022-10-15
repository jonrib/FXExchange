using System.Collections.Generic;
using FXExchange.Model.Exceptions;
using FXExchange.Service.CurrencyExchange;
using FXExchange.Service.CurrencyLoader;
using Moq;
using NUnit.Framework;

namespace UnitTests;

public class CurrencyExchangeServiceTest
{
    private Mock<ICurrencyLoader> _currencyLoaderMock = null!;

    [SetUp]
    public void Setup()
    {
        _currencyLoaderMock = new Mock<ICurrencyLoader>();
        _currencyLoaderMock.Setup(x => x.LoadCurrencies()).Returns(new Dictionary<string, decimal>
            {{"EUR/USD", 100}, {"USD/USD", 1}, {"EUR/EUR", 1}, {"USD/EUR", new decimal(1 / 100)}});
    }

    [Test]
    public void ExchangeCurrencies_Exchange10EurToUsd_Returns1000()
    {
        var service = new CurrencyExchange(_currencyLoaderMock.Object);

        Assert.AreEqual(service.ExchangeCurrencies("EUR/USD", 10), 1000);
    }

    [Test]
    public void ExchangeCurrencies_Exchange1EurToEur_Returns1()
    {
        var service = new CurrencyExchange(_currencyLoaderMock.Object);

        Assert.AreEqual(service.ExchangeCurrencies("EUR/EUR", 1), 1);
    }

    [Test]
    public void ExchangeCurrencies_ExchangeInvalidAmount_ThrownInvalidExchangeAmountException()
    {
        var service = new CurrencyExchange(_currencyLoaderMock.Object);

        Assert.Throws<InvalidExchangeAmountException>(() => service.ExchangeCurrencies("EUR/EUR", 0));
    }

    [Test]
    public void ExchangeCurrencies_ExchangeUnknownCurrency_ThrownUnknownExchangeRateException()
    {
        var service = new CurrencyExchange(_currencyLoaderMock.Object);

        Assert.Throws<ExchangeRateUnknownException>(() => service.ExchangeCurrencies("Euro/EUR", 100));
    }
}