using Dapper;
using ForeignExchange.Api.Database;
using ForeignExchange.Api.Models;

namespace ForeignExchange.Api.Repositories;

public class RatesRepository : IRatesRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public RatesRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    public async Task<FxRate?> GetRateAsync(string baseCurrency, string quoteCurrency)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        return await connection.QueryFirstOrDefaultAsync<FxRate?>(
            "SELECT * from FxRates WHERE FromCurrency=@baseCurrency AND ToCurrency=@quoteCurrency" +
            " ORDER BY TimestampUtc DESC LIMIT 1", new {baseCurrency, quoteCurrency});
    }
}
