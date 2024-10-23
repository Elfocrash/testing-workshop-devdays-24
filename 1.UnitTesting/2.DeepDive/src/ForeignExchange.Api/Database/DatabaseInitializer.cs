using Dapper;

namespace ForeignExchange.Api.Database;

public class DatabaseInitializer
{
    private readonly IDbConnectionFactory _connectionFactory;

    public DatabaseInitializer(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task InitializeAsync()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var created = await connection.ExecuteAsync(@"CREATE TABLE IF NOT EXISTS FxRates (
        FromCurrency TEXT NOT NULL, 
        ToCurrency TEXT NOT NULL,
        Rate DECIMAL NOT NULL,
        TimestampUtc timestamp  without time zone default (now() at time zone 'utc'),
        PRIMARY KEY(FromCurrency, ToCurrency))");
        
        await SeedFxRates();
    }

    private async Task SeedFxRates()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        await connection.ExecuteAsync(
            "INSERT INTO FxRates (FromCurrency, ToCurrency, Rate, TimestampUtc) VALUES ('GBP', 'USD', 1.1673908, NOW()::timestamp) ON CONFLICT DO NOTHING;" +
            "INSERT INTO FxRates (FromCurrency, ToCurrency, Rate, TimestampUtc) VALUES ('GBP', 'EUR', 1.1536164, NOW()::timestamp) ON CONFLICT DO NOTHING;" +
            "INSERT INTO FxRates (FromCurrency, ToCurrency, Rate, TimestampUtc) VALUES ('GBP', 'CAD', 1.5152764, NOW()::timestamp) ON CONFLICT DO NOTHING;" +
            "INSERT INTO FxRates (FromCurrency, ToCurrency, Rate, TimestampUtc) VALUES ('GBP', 'AUD', 1.6978968, NOW()::timestamp) ON CONFLICT DO NOTHING;" +
            
            "INSERT INTO FxRates (FromCurrency, ToCurrency, Rate, TimestampUtc) VALUES ('USD', 'GBP', 0.85636951, NOW()::timestamp) ON CONFLICT DO NOTHING;" +
            "INSERT INTO FxRates (FromCurrency, ToCurrency, Rate, TimestampUtc) VALUES ('USD', 'EUR', 0.98835877, NOW()::timestamp) ON CONFLICT DO NOTHING;" +
            "INSERT INTO FxRates (FromCurrency, ToCurrency, Rate, TimestampUtc) VALUES ('USD', 'CAD', 1.2988101, NOW()::timestamp) ON CONFLICT DO NOTHING;" +
            "INSERT INTO FxRates (FromCurrency, ToCurrency, Rate, TimestampUtc) VALUES ('USD', 'AUD', 1.4549404, NOW()::timestamp) ON CONFLICT DO NOTHING;" +
            
            "INSERT INTO FxRates (FromCurrency, ToCurrency, Rate, TimestampUtc) VALUES ('CAD', 'USD', 0.77000857, NOW()::timestamp) ON CONFLICT DO NOTHING;" +
            "INSERT INTO FxRates (FromCurrency, ToCurrency, Rate, TimestampUtc) VALUES ('CAD', 'GBP', 0.6598261, NOW()::timestamp) ON CONFLICT DO NOTHING;" +
            "INSERT INTO FxRates (FromCurrency, ToCurrency, Rate, TimestampUtc) VALUES ('CAD', 'EUR', 0.76140793, NOW()::timestamp) ON CONFLICT DO NOTHING;" +
            "INSERT INTO FxRates (FromCurrency, ToCurrency, Rate, TimestampUtc) VALUES ('CAD', 'AUD', 1.1202384, NOW()::timestamp) ON CONFLICT DO NOTHING;" +
            
            "INSERT INTO FxRates (FromCurrency, ToCurrency, Rate, TimestampUtc) VALUES ('EUR', 'GBP', 0.86683924, NOW()::timestamp) ON CONFLICT DO NOTHING;" +
            "INSERT INTO FxRates (FromCurrency, ToCurrency, Rate, TimestampUtc) VALUES ('EUR', 'USD', 1.0117783, NOW()::timestamp) ON CONFLICT DO NOTHING;" +
            "INSERT INTO FxRates (FromCurrency, ToCurrency, Rate, TimestampUtc) VALUES ('EUR', 'CAD', 1.313154, NOW()::timestamp) ON CONFLICT DO NOTHING;" + 
            "INSERT INTO FxRates (FromCurrency, ToCurrency, Rate, TimestampUtc) VALUES ('EUR', 'AUD', 1.471188, NOW()::timestamp) ON CONFLICT DO NOTHING;"
            );
    }
}
