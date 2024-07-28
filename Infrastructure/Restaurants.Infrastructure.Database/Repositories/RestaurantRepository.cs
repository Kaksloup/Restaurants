using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Repositories.Extensions;

namespace Restaurants.Infrastructure.Database;

public class RestaurantRepository(RestaurantsDbContext restaurantsDbContext) : IRestaurantsRepository
{
    public async Task<(List<Restaurant>, int)> GetRestaurantsAsync(string? searchPhrase, int pageSize,
        int pageNumber, Dictionary<string, SortingDirection>? sortingDirections)
    {
        var query = restaurantsDbContext.Restaurants
            .AsNoTracking()
            .Include(r => r.Dishes)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchPhrase))
            query = query.Where(x =>
                x.Name.ToLower().Contains(searchPhrase.ToLower()) ||
                x.Description.ToLower().Contains(searchPhrase.ToLower()));
        if (sortingDirections?.Count > 0)
            query = query.ApplySorting(sortingDirections);

        var totalValues = await query.CountAsync();

        return (await query
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(), totalValues);
    }

    public async Task<Restaurant?> GetRestaurantByIdAsync(int restaurantId)
    {
        return await restaurantsDbContext.Restaurants
            .Include(r => r.Address)
            .Include(r => r.Dishes)
            .FirstOrDefaultAsync(x => x.Id == restaurantId);
    }

    public bool CheckIfRestaurantExisteById(int restaurantId)
    {
        return restaurantsDbContext.Restaurants
            .AsNoTracking()
            .Any(r => r.Id == restaurantId);
    }

    public async Task<int> CreateRestaurantAsync(Restaurant restaurant)
    {
        var restaurantAdded = await restaurantsDbContext.Restaurants.AddAsync(restaurant);
        await restaurantsDbContext.SaveChangesAsync();
        return restaurantAdded.Entity.Id;
    }

    public async Task<int> DeleteRestaurantAsync(Restaurant restaurant)
    {
        var restaurantDeleted = restaurantsDbContext.Restaurants.Remove(restaurant);
        await restaurantsDbContext.SaveChangesAsync();
        return restaurantDeleted.Entity.Id;
    }

    public async Task<int> UpdateRestaurantAsync(Restaurant restaurant)
    {
        var restaurantUpdated = restaurantsDbContext.Restaurants.Update(restaurant);
        await restaurantsDbContext.SaveChangesAsync();
        return restaurantUpdated.Entity.Id;
    }
}