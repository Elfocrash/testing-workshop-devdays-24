using Customers.WebApp.Data;
using Customers.WebApp.Models;

namespace Customers.WebApp.Mapping;

public static class ModelToDtoMapper
{
    public static CustomerDto ToCustomerDto(this Customer customer)
    {
        return new CustomerDto
        {
            Id = customer.Id,
            Email = customer.Email,
            GitHubUsername = customer.GitHubUsername,
            FullName = customer.FullName,
            DateOfBirth = customer.DateOfBirth.ToDateTime(TimeOnly.MinValue)
        };
    }
}
