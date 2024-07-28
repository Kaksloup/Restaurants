using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Application.Constants;

namespace Restaurants.Infrastructure.Application.Authorization;

public class CustomUserClaimsPrincipalFactory(
    UserManager<User> userManager,
    RoleManager<Role> roleManager,
    IOptions<IdentityOptions> options)
    : UserClaimsPrincipalFactory<User, Role>(userManager, roleManager, options)
{
    public override async Task<ClaimsPrincipal> CreateAsync(User user)
    {
        var claims = await GenerateClaimsAsync(user);

        if (!string.IsNullOrWhiteSpace(user.FirstName))
            claims.AddClaim(new Claim(AppClaimTypes.FirstName, user.FirstName));

        if (user.BirthDate is not null)
            claims.AddClaim(new Claim(AppClaimTypes.DateOfBirth, user.BirthDate.Value.ToString("yyyy-MM-dd")));

        return new ClaimsPrincipal(claims);
    }
}