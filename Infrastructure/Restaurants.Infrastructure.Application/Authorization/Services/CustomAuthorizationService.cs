using Restaurants.Application.Bussiness;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Entities.Constants;
using Restaurants.Domain.Services;

namespace Restaurants.Infrastructure.Application.Authorization.Services;

public class CustomAuthorizationService(IHttpContextUserService contextUserService) : ICustomAuthorizationService
{
    public bool Authorize(Restaurant restaurant, RessourceOperation operation)
    {
        var currentUser = contextUserService.GetCurrentUser();
        if (operation is RessourceOperation.Read or RessourceOperation.Create) return true;

        if (currentUser != null && operation == RessourceOperation.Delete &&
            currentUser.Roles.Contains(UtilRoles.Administrator)) return true;

        return currentUser != null &&
               operation is RessourceOperation.Delete or RessourceOperation.Update
               && currentUser.Id == restaurant.OwnerId;
    }
}