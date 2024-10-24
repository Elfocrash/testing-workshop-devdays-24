using Xunit;

namespace Customers.Api.Tests.Integration;

[CollectionDefinition("Shared collection")]
public class SharedTestCollection : ICollectionFixture<CustomerApiFactory>
{
    
}
