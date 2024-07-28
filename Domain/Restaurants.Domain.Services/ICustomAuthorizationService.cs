using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Services;

public interface ICustomAuthorizationService
{
    public bool Authorize(Restaurant restaurant, RessourceOperation operation);
}