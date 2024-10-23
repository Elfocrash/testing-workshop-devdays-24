using System.ComponentModel.DataAnnotations;

namespace Customers.WebApp.Validation;

public class CustomerDate : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var date = (DateOnly)value!;
        var isValid = date <= DateOnly.FromDateTime(DateTime.Now);
        if (!isValid)
        {
            return new ValidationResult("Date of birth cannot be in the future",
                new[] { validationContext.MemberName }!);
        }

        return null;
    }
}
