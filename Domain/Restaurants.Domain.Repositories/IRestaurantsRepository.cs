using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories.Extensions;

namespace Restaurants.Domain.Repositories;

public interface IRestaurantsRepository
{
    Task<(List<Restaurant>, int)> GetRestaurantsAsync(string? searchPhrase, int pageSize, int pageNumber,
        Dictionary<string, SortingDirection>? sortingDirections);

    Task<Restaurant?> GetRestaurantByIdAsync(int restaurantId);

    bool CheckIfRestaurantExisteById(int restaurantId);

    Task<int> CreateRestaurantAsync(Restaurant restaurant);

    Task<int> DeleteRestaurantAsync(Restaurant restaurant);

    Task<int> UpdateRestaurantAsync(Restaurant restaurant);
}