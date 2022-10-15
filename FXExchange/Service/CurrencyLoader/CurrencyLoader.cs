using FXExchange.Service.DataParser;
using Microsoft.Extensions.Logging;

namespace FXExchange.Service.CurrencyLoader;

public class CurrencyLoader: ICurrencyLoader
{
    private readonly ILogger<CurrencyLoader> _logger;
    private readonly IDataParser _dataParser;

    public CurrencyLoader(ILogger<CurrencyLoader> logger, IDataParser dataParser)
    {
        _logger = logger;
        _dataParser = dataParser;
    }

    //NOTE: method could possible be reworked to calculate only the needed exchange rate and put it in memory for later...
    public IDictionary<string, decimal> LoadCurrencies()
    {
        var result = new Dictionary<string, decimal>();

        var data = _dataParser.ParseCurrencies().ToList();

        for (var i = 0; i < data.Count; i++)
        {
            try
            {
                //IMPORTANT: will no longer hold true if exchange rates are not provided in DKK
                result.Add($"{data[i].ISO}/DKK", data[i].Amount / 100);

                result.Add($"{data[i].ISO}/{data[i].ISO}", 1);

                for (var j = i+1; j < data.Count; j++)
                {
                    result.Add($"{data[i].ISO}/{data[j].ISO}", data[i].Amount / data[j].Amount);
                    result.Add($"{data[j].ISO}/{data[i].ISO}", data[j].Amount / data[i].Amount);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Failed to load currency for {data[i].ISO}");
            }
        }

        return result;
    }
}