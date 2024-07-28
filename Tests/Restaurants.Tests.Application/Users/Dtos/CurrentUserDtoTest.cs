using FluentAssertions;
using JetBrains.Annotations;
using Restaurants.Application.Bussiness;
using Restaurants.Domain.Entities.Constants;

namespace Restaurants.Tests.Application.Users.Dtos;

[TestSubject(typeof(CurrentUserDto))]
public class CurrentUserDtoTest
{
    [Theory]
    [InlineData(UtilRoles.Administrator)]
    [InlineData(UtilRoles.User)]
    public void IsInRole_WithMatchingRole_ShouldReturnTrue(string role)
    {
        // Arrange
        var currentUser = new CurrentUserDto(1, "", [UtilRoles.User, UtilRoles.Administrator], DateOnly.MinValue);
        // Act 
        var check = currentUser.IsInRole(role);
        // Assert 
        check.Should().BeTrue();
    }

    [Fact]
    public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
    {
        // Arrange
        var currentUser = new CurrentUserDto(1, "", [UtilRoles.User, UtilRoles.Administrator], DateOnly.MinValue);
        // Act 
        var check = currentUser.IsInRole(UtilRoles.Owner);
        // Assert 
        check.Should().BeFalse();
    }

    [Fact]
    public void IsInRole_WithNoMatchingRoleCase_ShouldReturnFalse()
    {
        // Arrange
        var currentUser = new CurrentUserDto(1, "", [UtilRoles.User, UtilRoles.Administrator], DateOnly.MinValue);
        // Act 
        var check = currentUser.IsInRole(UtilRoles.Administrator.ToLower());
        // Assert 
        check.Should().BeFalse();
    }
}