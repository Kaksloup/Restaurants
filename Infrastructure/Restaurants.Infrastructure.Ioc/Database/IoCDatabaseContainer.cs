using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Database;

namespace Restaurants.Infrastructure.Ioc;

public static class IoCDatabaseContainer
{
    /// <summary>
    ///     Configuration de la connexion de la base de données
    /// </summary>
    /// <param name="services"></param>
    /// <param name="connectionString">la chaine de connection à la base de données</param>
    /// <returns></returns>
    public static IServiceCollection AddDatabaseInfrastructure(this IServiceCollection services,
        string? connectionString)
    {
        return services.AddDbContext<RestaurantsDbContext>(options => options.UseSqlServer(connectionString))
            .AddScoped<IRestaurantsSeeder, RestaurantsSeeder>();
    }

    /// <summary>
    ///     Configuration de la connexion à la base identity
    /// </summary>
    /// <param name="services"></param>
    /// <param name="connectionString"></param>
    /// <returns></returns>
    public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services,
        string? connectionString)
    {
        return services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(connectionString));
    }

    /// <summary>
    ///     Configure les repositories
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddRepositoriesInfrastructure(this IServiceCollection services)
    {
        return services.AddScoped<IRestaurantsRepository, RestaurantRepository>()
            .AddScoped<IDishRepository, DishRepository>();
    }
}