using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IDishRepository
{
    Task<int> CreateDishAsync(Dish dish);
    Task<Dish?> GetDishByIdAndRestaurantIdAsync(int dishId, int restaurantId);
    Task<List<Dish>> GetDishesByRestaurantIdAsync(int restaurantId);
    Task DeleteDishes(IEnumerable<Dish> entities);
    bool CheckIfDishExisteById(int dishId);
}