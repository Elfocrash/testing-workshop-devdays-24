namespace ForeignExchange.Api.Validation;

[Serializable]
public class SameCurrencyException : ValidationException
{
    public SameCurrencyException(string currency) 
        : base("Currency", $"You cannot convert currency {currency} to itself")
    {

    }
}
