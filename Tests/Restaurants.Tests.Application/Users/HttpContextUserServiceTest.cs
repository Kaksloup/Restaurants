using System.Security.Claims;
using FluentAssertions;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Moq;
using Restaurants.Application.Bussiness;
using Restaurants.Domain.Entities.Constants;

namespace Restaurants.Tests.Application.Users;

[TestSubject(typeof(HttpContextUserService))]
public class HttpContextUserServiceTest
{
    [Fact]
    public void GetCurrentUser_WithAuthenticateUser_ShouldReturnCurrentUser()
    {
        // Arrange
        const string email = "test@gmail.com";
        var dateOfBirth = new DateOnly(1997, 9, 15);

        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, "1"),
            new(ClaimTypes.Email, email),
            new(ClaimTypes.Role, UtilRoles.Administrator),
            new(ClaimTypes.Role, UtilRoles.User),
            new("DateOfBirth", dateOfBirth.ToString("yyyy-MM-dd"))
        };
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));
        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext
        {
            User = user
        });

        var userContext = new HttpContextUserService(httpContextAccessorMock.Object);
        // Act
        var currentUser = userContext.GetCurrentUser();

        // Asset
        currentUser.Should().NotBeNull();
        currentUser?.Id.Should().Be(1);
        currentUser?.Email.Should().Be(email);
        currentUser?.Roles.Should().ContainInConsecutiveOrder(UtilRoles.Administrator, UtilRoles.User);
        currentUser?.DateOfBirth.Should().Be(dateOfBirth);
    }

    [Fact]
    public void GetCurrentUser_WithUserContextNotPresent_ShouldThrowsInvalidOperationException()
    {
        // Arrange
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext?)null);

        var userContext = new HttpContextUserService(httpContextAccessorMock.Object);

        // Act
        Action action = () => userContext.GetCurrentUser();

        // Asset
        action.Should().Throw<InvalidOperationException>()
            .WithMessage("User context is not present");
    }
}