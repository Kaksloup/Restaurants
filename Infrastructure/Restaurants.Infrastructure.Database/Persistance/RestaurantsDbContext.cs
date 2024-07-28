using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Database;

public class RestaurantsDbContext(DbContextOptions<RestaurantsDbContext> options) : DbContext(options)
{
    internal virtual DbSet<Address> Addresses { get; set; }
    internal virtual DbSet<Restaurant> Restaurants { get; set; }
    internal virtual DbSet<Dish> Dishes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}