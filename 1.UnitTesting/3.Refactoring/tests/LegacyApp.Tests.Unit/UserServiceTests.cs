using System;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace LegacyApp.Tests.Unit;

public class UserServiceTests
{
    private readonly TimeProvider _timeProvider = Substitute.For<TimeProvider>();
    private readonly IClientRepository _clientRepository = Substitute.For<IClientRepository>();

    private readonly IUserCreditServiceClientFactory _userCreditServiceClientFactory =
        Substitute.For<IUserCreditServiceClientFactory>();

    private readonly IUserDataAccessAdapter _accessAdapter = Substitute.For<IUserDataAccessAdapter>();
    
    private readonly UserService _sut;

    public UserServiceTests()
    {
        _timeProvider.LocalTimeZone.Returns(TimeZoneInfo.Utc);
        _sut = new UserService(_timeProvider, _clientRepository, _userCreditServiceClientFactory, _accessAdapter);
    }
    
    [Fact]
    void AddUser_ShouldNotCreateUser_WhenFirstNameIsEmpty()
    {
        // Arrange
        var firstName = string.Empty;
        var surName = "Chapsas";
        var email = "nick@dometrain.com";
        var dob = new DateTime(1993, 1, 1);
        var clientId = 60;
        
        // Act
        var result = _sut.AddUser(firstName, surName, email, dob, clientId);
                                  
        // Assert
        _accessAdapter.DidNotReceive().AddUser(Arg.Any<User>());
        result.Should().BeFalse();
    }

    [Fact]
    public void AddUser_ShouldNotCreateUser_WhenLastNameIsEmpty()
    {
        // Arrange
        var firstName = "Nick";
        var surName = string.Empty;
        var email = "nick@dometrain.com";
        var dob = new DateTime(1993, 1, 1);
        var clientId = 60;
        
        // Act
        var result = _sut.AddUser(firstName, surName, email, dob, clientId);
                                  
        // Assert
        _accessAdapter.DidNotReceive().AddUser(Arg.Any<User>());
        result.Should().BeFalse();
    }

    [Fact]
    public void AddUser_ShouldNotCreateUser_WhenEmailIsInvalid()
    {
        // Arrange
        var firstName = "Nick";
        var surName = "Chapsas";
        var email = "nickom";
        var dob = new DateTime(1993, 1, 1);
        var clientId = 60;
        
        // Act
        var result = _sut.AddUser(firstName, surName, email, dob, clientId);
                                  
        // Assert
        _accessAdapter.DidNotReceive().AddUser(Arg.Any<User>());
        result.Should().BeFalse();
    }

    [Fact]
    public void AddUser_ShouldNotCreateUser_WhenUserIsUnder21()
    {
        // Arrange
        var firstName = "Nick";
        var surName = "Chapsas";
        var email = "nick@dometrain.com";
        var dob = new DateTime(2004, 1, 1);
        var clientId = 60;
        _timeProvider.GetUtcNow().Returns(new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero));
        
        // Act
        var result = _sut.AddUser(firstName, surName, email, dob, clientId);
                                  
        // Assert
        _accessAdapter.DidNotReceive().AddUser(Arg.Any<User>());
        result.Should().BeFalse();
    }

    [Fact]
    public void AddUser_ShouldNotCreateUser_WhenUserHasCreditLimitAndLimitIsLessThan500()
    {
        // Arrange
        var firstName = "Nick";
        var surName = "Chaspas";
        var email = "nick@dometrain.com";
        var dob = new DateTime(1993, 1, 1);
        var clientId = 60;
        _timeProvider.GetUtcNow().Returns(new DateTime(2024, 1, 1));
        
        var client = new Client
        {
            Id = clientId,
            Name = "Nick Chapsas Ltd",
            ClientStatus = ClientStatus.Gold
        };

        _clientRepository.GetById(clientId).Returns(client);
        var userServiceClient = Substitute.For<IUserCreditService>();
        userServiceClient.GetCreditLimit(firstName, surName, dob).Returns(499);
        _userCreditServiceClientFactory.CreateClient().Returns(userServiceClient);

        // Act
        var result = _sut.AddUser(firstName, surName, email, dob, clientId);
                                  
        // Assert
        _accessAdapter.DidNotReceive().AddUser(Arg.Any<User>());
        result.Should().BeFalse();
    }
    
    [Fact]
    public void AddUser_ShouldCreateUser_WhenUserDetailsAreValid()
    {
        // Arrange
        var firstName = "Nick";
        var surName = "Chaspas";
        var email = "nick@dometrain.com";
        var dob = new DateTime(1993, 1, 1);
        var clientId = 60;
        _timeProvider.GetUtcNow().Returns(new DateTime(2024, 1, 1));
        
        var client = new Client
        {
            Id = clientId,
            Name = "Nick Chapsas Ltd",
            ClientStatus = ClientStatus.Gold
        };

        _clientRepository.GetById(clientId).Returns(client);
        var userServiceClient = Substitute.For<IUserCreditService>();
        userServiceClient.GetCreditLimit(firstName, surName, dob).Returns(500);
        _userCreditServiceClientFactory.CreateClient().Returns(userServiceClient);

        // Act
        var result = _sut.AddUser(firstName, surName, email, dob, clientId);
                                  
        // Assert
        result.Should().BeTrue();
        _accessAdapter.Received(1).AddUser(Arg.Is<User>(x => x.Firstname == firstName));
    }
}
