using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.Bussiness;

namespace Restaurants.Infrastructure.Application.Authorization.Requirements;

public class MinimumAgeRequirementHandler(IHttpContextUserService userContext)
    : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        MinimumAgeRequirement requirement)
    {
        var currentUser = userContext.GetCurrentUser();
        if (currentUser is not null && currentUser.DateOfBirth.AddYears(requirement.MinimumAge) <=
            DateOnly.FromDateTime(DateTime.Now))
            context.Succeed(requirement);
        else
            context.Fail();

        return Task.CompletedTask;
    }
}