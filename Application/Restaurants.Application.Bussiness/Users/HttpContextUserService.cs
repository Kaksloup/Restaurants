using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Restaurants.Application.Bussiness;

public class HttpContextUserService(IHttpContextAccessor httpContextAccessor) : IHttpContextUserService
{
    public CurrentUserDto? GetCurrentUser()
    {
        var currentUser = httpContextAccessor.HttpContext?.User ??
                          throw new InvalidOperationException("User context is not present");

        if (currentUser.Identity == null || !currentUser.Identity.IsAuthenticated) return null;
        var userId = currentUser.FindFirstValue(ClaimTypes.NameIdentifier);
        _ = int.TryParse(userId, out var id);
        var userEmail = currentUser.FindFirstValue(ClaimTypes.Email);
        var roles = currentUser.Claims.Where(x => x.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        var dateOfBirth = currentUser.FindFirst(c => c.Type == "DateOfBirth")?.Value ??
                          DateTime.Now.ToString("yyyy-MM-dd");

        return new CurrentUserDto(id, userEmail, roles, DateOnly.ParseExact(dateOfBirth, "yyyy-MM-dd"));
    }
}