using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Customers.WebApp.Validation;

public class CustomerEmail : ValidationAttribute
{
    private static readonly Regex GithubRegex = new("^[\\w!#$%&’*+/=?`{|}~^-]+(?:\\.[\\w!#$%&’*+/=?`{|}~^-]+)*@(?:[a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {

        var username = (string)(value ?? string.Empty);
        if (!GithubRegex.IsMatch(username))
        {
            return new ValidationResult("Invalid email format",
                new[] { validationContext.MemberName }!);
        }

        return null;
    }
}
