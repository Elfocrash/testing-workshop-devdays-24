namespace ForeignExchange.Api.Models;

public class ConversionQuote
{
    public string BaseCurrency { get; init; } = default!;
    
    public string QuoteCurrency { get; init; } = default!;

    public decimal BaseAmount { get; init; }
    
    public decimal QuoteAmount { get; init; }
}
