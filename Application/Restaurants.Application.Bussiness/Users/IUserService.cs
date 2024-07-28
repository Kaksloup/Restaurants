namespace Restaurants.Application.Bussiness;

public interface IUserService
{
    Task UpdateUserAsync(UpdateUserCommand command, CancellationToken cancellationToken);
}