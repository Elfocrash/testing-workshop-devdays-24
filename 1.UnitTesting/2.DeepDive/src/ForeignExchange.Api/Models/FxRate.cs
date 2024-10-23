namespace ForeignExchange.Api.Models;

public class FxRate
{
    public string FromCurrency { get; init; } = default!;

    public string ToCurrency { get; init; } = default!;

    public decimal Rate { get; init; }

    public DateTime TimestampUtc { get; init; }
}
