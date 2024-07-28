using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Database;

public class DishRepository(RestaurantsDbContext restaurantsDbContext) : IDishRepository
{
    public async Task<int> CreateDishAsync(Dish dish)
    {
        var dishAdded = await restaurantsDbContext.Dishes.AddAsync(dish);
        await restaurantsDbContext.SaveChangesAsync();
        return dishAdded.Entity.Id;
    }

    public async Task<Dish?> GetDishByIdAndRestaurantIdAsync(int dishId, int restaurantId)
    {
        return await restaurantsDbContext.Dishes.FirstOrDefaultAsync(dish =>
            dish.Id == dishId && dish.RestaurantId == restaurantId);
    }

    public async Task<List<Dish>> GetDishesByRestaurantIdAsync(int restaurantId)
    {
        return await restaurantsDbContext.Dishes.Where(dish => dish.RestaurantId == restaurantId)
            .ToListAsync();
    }

    public async Task DeleteDishes(IEnumerable<Dish> entities)
    {
        restaurantsDbContext.Dishes.RemoveRange(entities);
        await restaurantsDbContext.SaveChangesAsync();
    }

    public bool CheckIfDishExisteById(int dishId)
    {
        return restaurantsDbContext.Dishes
            .AsNoTracking()
            .Any(r => r.Id == dishId);
    }
}