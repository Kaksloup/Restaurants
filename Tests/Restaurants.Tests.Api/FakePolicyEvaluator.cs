using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Restaurants.Domain.Entities.Constants;

namespace Restaurants.Tests.Api;

public class FakePolicyEvaluator : IPolicyEvaluator
{
    public Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
    {
        const string email = "test@gmail.com";
        var dateOfBirth = new DateOnly(1997, 9, 15);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, "1"),
            new(ClaimTypes.Email, email),
            new(ClaimTypes.Role, UtilRoles.Administrator),
            new(ClaimTypes.Role, UtilRoles.User),
            new("DateOfBirth", dateOfBirth.ToString("yyyy-MM-dd"))
        };
        var userClaimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));
        var ticket = new AuthenticationTicket(userClaimsPrincipal, "Test");
        var result = AuthenticateResult.Success(ticket);
        return Task.FromResult(result);
    }

    public Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy,
        AuthenticateResult authenticationResult, HttpContext context,
        object? resource)
    {
        var result = PolicyAuthorizationResult.Success();
        return Task.FromResult(result);
    }
}