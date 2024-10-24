using ForeignExchange.Api.Models;
using ForeignExchange.Api.Repositories;

namespace ForeignExchange.Api.Tests.Unit;

public class FakeRatesRepository : IRatesRepository
{
    public async Task<FxRate?> GetRateAsync(string baseCurrency, string quoteCurrency)
    {

        return new FxRate
        {
            FromCurrency = baseCurrency,
            ToCurrency = quoteCurrency,
            Rate = 1.6m
        };
    }
}
