using System.Net;
using System.Net.Http.Json;
using Customers.Api.Contracts.Requests;
using Customers.Api.Contracts.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Customers.Api.Tests.Integration;
 
public class CustomerEndpointsTests : IAsyncLifetime
{
    private readonly WebApplicationFactory<IApiMarker> _waf = new();
    private readonly List<Guid> _idsToDelete = [];
    private readonly HttpClient _client;

    public CustomerEndpointsTests()
    {
        _client = _waf.CreateClient();
    }
    
    [Fact]
    public async Task Create_ShouldCreateCustomer_WhenDetailsAreValid()
    {
        // Arrange
        var request = new CustomerRequest
        {
            Email = "nick@chapsas.com",
            FullName = "Nick Chapsas",
            DateOfBirth = new DateTime(1993, 01, 01),
            GitHubUsername = "nickchapsas"
        };

        var expectedResponse = new CustomerResponse
        {
            Email = request.Email,
            FullName = request.FullName,
            DateOfBirth = request.DateOfBirth,
            GitHubUsername = request.GitHubUsername
        };

        // Act
        var response = await _client.PostAsJsonAsync("customers", request);
        var customerResponse = await response.Content.ReadFromJsonAsync<CustomerResponse>();
        _idsToDelete.Add(customerResponse!.Id);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        customerResponse.Should().BeEquivalentTo(expectedResponse, x => x.Excluding(r => r.Id));
        customerResponse!.Id.Should().NotBeEmpty();
        response.Headers.Location.Should().Be($"http://localhost/customers/{customerResponse!.Id}");
    }
    
    [Fact]
    public async Task Get_ShouldReturnCustomer_WhenCustomerExists()
    {
        // Arrange
        var request = new CustomerRequest
        {
            Email = "nick@chapsas.com",
            FullName = "Nick Chapsas",
            DateOfBirth = new DateTime(1993, 01, 01),
            GitHubUsername = "nickchapsas"
        };

        var createCustomerHttpResponse = await _client.PostAsJsonAsync("customers", request);
        var createdCustomer = await createCustomerHttpResponse.Content.ReadFromJsonAsync<CustomerResponse>();

        // Act
        var response = await _client.GetAsync($"customers/{createdCustomer!.Id}");
        var customerResponse = await response.Content.ReadFromJsonAsync<CustomerResponse>();
        _idsToDelete.Add(customerResponse!.Id);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        customerResponse.Should().BeEquivalentTo(createdCustomer);
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        foreach (var id in _idsToDelete)
        {
            await _client.DeleteAsync($"customers/{id}");
        }
    }
}
