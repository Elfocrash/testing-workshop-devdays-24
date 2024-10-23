using ForeignExchange.Api.Models;

namespace ForeignExchange.Api.Services;

public interface IQuoteService
{
    Task<ConversionQuote?> GetQuoteAsync(string fromCurrency, string toCurrency, decimal amount);
}
