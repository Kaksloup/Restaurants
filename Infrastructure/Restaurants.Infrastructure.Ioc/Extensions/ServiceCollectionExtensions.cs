using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Services;
using Restaurants.Infrastructure.Application.Authorization.Services;
using Restaurants.Infrastructure.Ioc.BlobStorage;

namespace Restaurants.Infrastructure.Ioc;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("RestaurantsDb");
        services.AddDatabaseInfrastructure(connectionString)
            .AddIdentityInfrastructure(connectionString)
            .AddRepositoriesInfrastructure()
            .AddBlobStorageSettings(configuration)
            .AddBlobStorageServices()
            .AddScoped<ICustomAuthorizationService, CustomAuthorizationService>();
    }
}