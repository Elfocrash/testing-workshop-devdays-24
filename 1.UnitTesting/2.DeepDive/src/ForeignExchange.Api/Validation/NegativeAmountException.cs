namespace ForeignExchange.Api.Validation;

[Serializable]
public class NegativeAmountException : ValidationException
{
    public const string ErrorMessage = "You can only convert a positive amount of money!";
    
    public NegativeAmountException() 
        : base("Amount", ErrorMessage)
    {

    }
}
