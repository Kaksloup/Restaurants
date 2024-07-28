using Microsoft.Extensions.DependencyInjection;

namespace Restaurant.Application.Ioc;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddServicesInfrastructure()
            .AddAutomapper()
            .AddValidator()
            .AddMediatR()
            .AddHttpContextAccessor();
    }
}