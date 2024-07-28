namespace Restaurants.Application.Bussiness;

public interface IHttpContextUserService
{
    public CurrentUserDto? GetCurrentUser();
}