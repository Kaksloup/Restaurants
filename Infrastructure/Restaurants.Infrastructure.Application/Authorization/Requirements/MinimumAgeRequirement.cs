using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Application.Authorization.Requirements;

public class MinimumAgeRequirement(int minimumAge) : IAuthorizationRequirement
{
    public int MinimumAge { get; } = minimumAge;
}