using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Bussiness;

namespace Restaurant.Application.Ioc;

public static class IoCServicesContainer
{
    /// <summary>
    ///     Configure les services
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddServicesInfrastructure(this IServiceCollection services)
    {
        return services.AddScoped<IRestaurantService, RestaurantService>()
            .AddScoped<IDishService, DishService>()
            .AddScoped<IHttpContextUserService, HttpContextUserService>()
            .AddScoped<IUserService, UserService>();
    }

    /// <summary>
    ///     Configure le mapper
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddAutomapper(this IServiceCollection services)
    {
        return services.AddAutoMapper(Assembly.Load("Restaurants.Application.Bussiness"));
    }

    /// <summary>
    ///     Configure le validateur
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddValidator(this IServiceCollection services)
    {
        return services.AddValidatorsFromAssembly(Assembly.Load("Restaurants.Application.Bussiness"))
            .AddFluentValidationAutoValidation();
    }

    /// <summary>
    ///     Configure le mediatR
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        return services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.Load("Restaurants.Application.Bussiness")));
    }
}