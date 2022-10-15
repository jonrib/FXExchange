using System;
using System.Collections.Generic;
using FXExchange.Model.CurrencyExchange;
using FXExchange.Service.CurrencyExchange;
using FXExchange.Service.CurrencyLoader;
using FXExchange.Service.DataParser;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace UnitTests;

public class CurrencyLoaderTest
{
    private Mock<IDataParser> _dataSeederMock = null!;

    [SetUp]
    public void Setup()
    {
        _dataSeederMock = new Mock<IDataParser>();
        _dataSeederMock.Setup(x => x.ParseCurrencies()).Returns(new List<Currency>
        {
            new()
            {
                ISO = "EUR",
                Amount = 100,
                Name = "Euro"
            },
            new()
            {
                ISO = "USD",
                Amount = 12,
                Name = "American dollar"
            }
        });
    }

    //TODO test could validate exchange rates...
    [Test]
    public void LoadCurrencies_LoadEURUSDCurrencyExchangeRates_Returns6ExchangeRates()
    {
        var service = new CurrencyLoader(new Mock<ILogger<CurrencyLoader>>().Object, _dataSeederMock.Object);
        var loadedCurrencies = service.LoadCurrencies();

        Assert.AreEqual(loadedCurrencies.Count, 6);
    }

    //TODO maybe not the best way to assert something went wrong. Could instead check if exception was logged...
    [Test]
    public void LoadCurrencies_LoadEURUSDInvalidCurrencyExchangeRates_Returns5ExchangeRates()
    {
        _dataSeederMock.Setup(x => x.ParseCurrencies()).Returns(new List<Currency>
        {
            new()
            {
                ISO = "EUR",
                Amount = 0,
                Name = "Euro"
            },
            new()
            {
                ISO = "USD",
                Amount = 12,
                Name = "American dollar"
            }
        });
        var service = new CurrencyLoader(new Mock<ILogger<CurrencyLoader>>().Object, _dataSeederMock.Object);

        var loadedCurrencies = service.LoadCurrencies();

        Assert.AreEqual(loadedCurrencies.Count, 5);
    }
}