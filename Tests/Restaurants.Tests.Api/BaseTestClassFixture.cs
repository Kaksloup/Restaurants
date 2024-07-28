using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Database;

namespace Restaurants.Tests.Api;

public class BaseTestClassFixture : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    protected readonly HttpClient Client;
    protected readonly Mock<IRestaurantsRepository> RestaurantsRepositoryMock = new();

    protected readonly Mock<IRestaurantsSeeder> RestaurantsSeederMock = new();

    public BaseTestClassFixture(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();

                services.Replace(ServiceDescriptor.Scoped(typeof(IRestaurantsRepository),
                    _ => RestaurantsRepositoryMock.Object));

                services.Replace(ServiceDescriptor.Scoped(typeof(IRestaurantsSeeder),
                    _ => RestaurantsSeederMock.Object));
            });
        });

        Client = _factory.CreateClient();
    }
}