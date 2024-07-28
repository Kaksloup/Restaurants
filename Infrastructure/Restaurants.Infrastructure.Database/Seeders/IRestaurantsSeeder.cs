namespace Restaurants.Infrastructure.Database;

public interface IRestaurantsSeeder
{
    Task SeedIdentityAsync();
    Task SeedRestaurantAsync();
}