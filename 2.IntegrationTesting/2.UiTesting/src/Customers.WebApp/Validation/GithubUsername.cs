using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Customers.WebApp.Validation;

public class GithubUsername : ValidationAttribute
{
    private static readonly Regex GithubRegex = new("^[a-z\\d](?:[a-z\\d]|-(?=[a-z\\d])){0,38}$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {

        var username = (string)(value ?? string.Empty);
        if (!GithubRegex.IsMatch(username))
        {
            return new ValidationResult("Invalid GitHub username format",
                new[] { validationContext.MemberName }!);
        }

        return null;
    }
}
