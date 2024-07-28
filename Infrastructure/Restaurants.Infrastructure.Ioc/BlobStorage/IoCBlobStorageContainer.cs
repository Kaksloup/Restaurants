using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Services;
using Restaurants.Infrastructure.BlobStorage;
using Restaurants.Infrastructure.BlobStorage.Configuration;

namespace Restaurants.Infrastructure.Ioc.BlobStorage;

public static class IoCBlobStorageContainer
{
    public static IServiceCollection AddBlobStorageSettings(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.Configure<BlobStorageSettings>(configuration.GetSection("BlobStorage"));
    }

    public static IServiceCollection AddBlobStorageServices(this IServiceCollection services)
    {
        return services.AddScoped<IBlobStorageService, BlobStorageService>();
    }
}