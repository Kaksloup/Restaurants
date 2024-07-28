namespace Restaurants.Application.Bussiness;

public record CurrentUserDto(int Id, string? Email, List<string> Roles, DateOnly DateOfBirth)
{
    public bool IsInRole(string role)
    {
        return Roles.Contains(role);
    }
}