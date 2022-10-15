using System.Linq;
using FXExchange.Service.DataParser;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace UnitTests;

public class DataParserTest
{
    //TODO test could generate a file with appropriate data and delete it after...
    [Test]
    public void ParseCurrencies_LoadCurrenciesFromFile_Returns7Currencies()
    {
        var service = new DataParser(new Mock<ILogger<DataParser>>().Object);
        var parsedCurrencies = service.ParseCurrencies().ToList();

        Assert.AreEqual(parsedCurrencies.Count, 7);
    }
}