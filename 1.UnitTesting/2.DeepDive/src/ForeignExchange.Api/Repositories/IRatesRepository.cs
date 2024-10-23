using ForeignExchange.Api.Models;

namespace ForeignExchange.Api.Repositories;

public interface IRatesRepository
{
    Task<FxRate?> GetRateAsync(string baseCurrency, string quoteCurrency);
}
