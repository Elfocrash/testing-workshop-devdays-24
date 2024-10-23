using Customers.WebApp.Data;
using Customers.WebApp.Models;

namespace Customers.WebApp.Mapping;

public static class DtoToModelMapper
{
    public static Customer ToCustomer(this CustomerDto customerDto)
    {
        return new Customer
        {
            Id = customerDto.Id,
            Email = customerDto.Email,
            GitHubUsername = customerDto.GitHubUsername,
            FullName = customerDto.FullName,
            DateOfBirth = DateOnly.FromDateTime(customerDto.DateOfBirth)
        };
    }
}
