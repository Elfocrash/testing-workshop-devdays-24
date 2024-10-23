using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Customers.WebApp.Validation;

namespace Customers.WebApp.Models;

public class Customer
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [GithubUsername]
    public string GitHubUsername { get; set; } = default!;

    [Required]
    [RegularExpression("(?i)^[a-z ,.'-]+$(?-i)", ErrorMessage = "Invalid fullname")]
    public string FullName { get; set; } = default!;

    [Required]
    [CustomerEmail]
    public string Email { get; set; } = default!;

    [Required]
    [CustomerDate]
    public DateOnly DateOfBirth { get; set; } = DateOnly.FromDateTime(DateTime.Now.Date);
}
