using System.Data;

namespace ForeignExchange.Api.Database;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateConnectionAsync();
}
