namespace ForeignExchange.Api.Validation;

[Serializable]
public class NegativeAmountException : ValidationException
{
    public NegativeAmountException() 
        : base("Amount", $"You can only convert a positive amount of money")
    {

    }
}
