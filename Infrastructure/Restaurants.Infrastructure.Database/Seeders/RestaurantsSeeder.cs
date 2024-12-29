using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Entities.Constants;

namespace Restaurants.Infrastructure.Database;

public class RestaurantsSeeder(RestaurantsDbContext restaurantsDbContext, IdentityDbContext identityDbContext)
    : IRestaurantsSeeder
{
    public async Task SeedIdentityAsync()
    {
        // Application de la migration automatique sur la base de données
        if (identityDbContext.Database.GetPendingMigrations().Any())
            await identityDbContext.Database.MigrateAsync();
        
        if (await identityDbContext.Database.CanConnectAsync() && !identityDbContext.Roles.Any())
        {
            await identityDbContext.Roles.AddRangeAsync(GetSeedRoles());
            await identityDbContext.SaveChangesAsync();
        }

        if (await identityDbContext.Database.CanConnectAsync() && !identityDbContext.Users.Any())
        {
            await identityDbContext.Users.AddRangeAsync(GetSeedUsers());
            await identityDbContext.SaveChangesAsync();
        }
        
        if (await identityDbContext.Database.CanConnectAsync() && !identityDbContext.UserRoles.Any())
        {
            await identityDbContext.UserRoles.AddRangeAsync(GetSeedUserRoles());
            await identityDbContext.SaveChangesAsync();
        }
    }

    public async Task SeedRestaurantAsync()
    {
        // Application de la migration automatique sur la base de données
        if (restaurantsDbContext.Database.GetPendingMigrations().Any())
            await restaurantsDbContext.Database.MigrateAsync();

        if (await restaurantsDbContext.Database.CanConnectAsync() && !restaurantsDbContext.Restaurants.Any())
        {
            await restaurantsDbContext.Restaurants.AddRangeAsync(GetSeedRestaurants());
            await restaurantsDbContext.SaveChangesAsync();
        }
    }

    private static List<Role> GetSeedRoles()
    {
        List<Role> roles =
        [
            new Role { Name = UtilRoles.User, NormalizedName = UtilRoles.User.ToUpper() },
            new Role { Name = UtilRoles.Owner, NormalizedName = UtilRoles.Owner.ToUpper() },
            new Role { Name = UtilRoles.Administrator, NormalizedName = UtilRoles.Administrator.ToUpper() }
        ];

        return roles;
    }

    private static List<User> GetSeedUsers()
    {
        List<User> users =
        [
            // MDP : Haha759.
            new User
            {
                UserName = "kake@admin.com",
                NormalizedUserName = "KAKE@ADMIN.COM",
                Email = "kake@admin.com",
                NormalizedEmail = "KAKE@ADMIN.COM",
                PasswordHash = "AQAAAAIAAYagAAAAENGZwoicLQgz1QxczEkBduVBeYbVOLfKxfc9Q2boSn558T5CYsSHqcdzi8//KswQ3g==",
                SecurityStamp = "KHTECUCNJYUWFWYF3VC3FHTTQZ6FGU3W",
                ConcurrencyStamp = "5a169dd4-8705-48e3-be44-cc29c6eada6a",
                LockoutEnabled = true,
            }
        ];

        return users;
    }
    
    private static List<UserRole> GetSeedUserRoles()
    {
        List<UserRole> userRoles =
        [
            new UserRole { UserId = 1, RoleId = 1},
            new UserRole { UserId = 1, RoleId = 2},
            new UserRole { UserId = 1, RoleId = 3},
        ];

        return userRoles;
    }

    private static List<Restaurant> GetSeedRestaurants()
    {
        var onwer = new User
        {
            Email = "test@admin.com"
        };
        
        List<Restaurant> restaurants =
        [
            new Restaurant
            {
                Name = "KFC",
                Category = "Fast Food",
                Description =
                    "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                ContactEmail = "contact@kfc.com",
                HasDelivery = true,
                Dishes =
                [
                    new Dish
                    {
                        Name = "Nashville Hot Chicken",
                        Description = "Nashville Hot Chicken (10 pcs.)",
                        Price = 10.30M
                    },

                    new Dish
                    {
                        Name = "Chicken Nuggets",
                        Description = "Chicken Nuggets (5 pcs.)",
                        Price = 5.30M
                    }
                ],
                Address = new Address
                {
                    City = "London",
                    Street = "Cork St 5",
                    PostalCode = "WC2N 5DU"
                },
                OwnerId = 1,
            },
            new Restaurant
            {
                Name = "McDonald",
                Category = "Fast Food",
                Description =
                    "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                ContactEmail = "contact@mcdonald.com",
                HasDelivery = true,
                Address = new Address
                {
                    City = "London",
                    Street = "Boots 193",
                    PostalCode = "W1F 8SR"
                },
                OwnerId = 1,
            }
        ];

        return restaurants;
    }
}