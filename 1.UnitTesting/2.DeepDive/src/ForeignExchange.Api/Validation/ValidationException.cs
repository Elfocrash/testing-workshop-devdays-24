namespace ForeignExchange.Api.Validation;

[Serializable]
public class ValidationException : Exception
{
    public string PropertyName { get; init; }
    
    public ValidationException(string propertyName, string message) : base(message)
    {
        PropertyName = propertyName;
    }
}
