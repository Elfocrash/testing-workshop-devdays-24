using System.Diagnostics;
using ForeignExchange.Api.Logging;
using ForeignExchange.Api.Models;
using ForeignExchange.Api.Repositories;
using ForeignExchange.Api.Validation;

namespace ForeignExchange.Api.Services;

public class QuoteService : IQuoteService
{
    private readonly IRatesRepository _ratesRepository;
    private readonly ILoggerAdapter<QuoteService> _logger;

    public QuoteService(IRatesRepository ratesRepository, ILoggerAdapter<QuoteService> logger)
    {
        _ratesRepository = ratesRepository;
        _logger = logger;
    }

    public async Task<ConversionQuote?> GetQuoteAsync(
        string fromCurrency, string toCurrency, decimal amount)
    {
        var sw = Stopwatch.StartNew();
        try
        {
            if (amount <= 0)
            {
                throw new NegativeAmountException();
            }
            
            if (fromCurrency == toCurrency)
            {
                throw new SameCurrencyException(fromCurrency);
            }
        
            var rate = await _ratesRepository.GetRateAsync(fromCurrency, toCurrency);

            if (rate is null)
            {
                return null;
            }

            var quoteAmount = rate.Rate * amount;
        
            return new ConversionQuote
            {
                BaseCurrency = fromCurrency,
                QuoteCurrency = toCurrency,
                BaseAmount = amount,
                QuoteAmount = quoteAmount
            };
        }
        finally
        {
            _logger.LogInformation(
                "Retrieved quote for currencies {FromCurrency}->{ToCurrency} in {ElapsedMilliseconds}ms",
                fromCurrency, toCurrency, sw.ElapsedMilliseconds);
        }
    }
}
