using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Restaurants.Application.Bussiness;
using Restaurants.Presentation.Api;

namespace Restaurants.Tests.Api.Controllers;

[TestSubject(typeof(RestaurantsController))]
public class RestaurantsControllerTest : BaseTestClassFixture
{
    public RestaurantsControllerTest(WebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [Fact]
    public async Task GetRestaurants_ForValidRequest_Returns200Ok()
    {
        // Arrange

        // Act
        var result = await Client.GetAsync("api/Restaurants?PageNumber=1&PageSize=10");
        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetRestaurants_ForInvalidRequest_Returns400BadRequest()
    {
        // Arrange

        // Act
        var result = await Client.GetAsync("api/Restaurants");

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Theory]
    [InlineData(1589)]
    public async Task GetRestaurantById_ForNonExistingId_ShouldReturn404NotFound(int restaurantId)
    {
        // Arrange
        RestaurantsRepositoryMock.Setup(repo => repo.GetRestaurantByIdAsync(restaurantId))
            .ReturnsAsync(null as Domain.Entities.Restaurant);

        // Act
        var result = await Client.GetAsync($"api/Restaurants/{restaurantId}");

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetRestaurantById_ForExistingId_ShouldReturn200Ok()
    {
        // Arrange
        const int restaurantId = 1;
        var restaurant = new Domain.Entities.Restaurant
        {
            Id = restaurantId,
            Name = "Test Restaurant",
            Description = "Ceci est un test de restaurant",
            Category = "Test",
            HasDelivery = false
        };

        RestaurantsRepositoryMock.Setup(repo => repo.GetRestaurantByIdAsync(restaurantId)).ReturnsAsync(restaurant);

        // Act
        var result = await Client.GetAsync($"api/Restaurants/{restaurantId}");
        var restaurantDto = await result.Content.ReadFromJsonAsync<ReadRestaurantDto>();

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        restaurantDto.Should().NotBeNull();
        restaurantDto?.Id.Should().Be(restaurantId);
        restaurantDto?.Name.Should().Be(restaurant.Name);
        restaurantDto?.Description.Should().Be(restaurant.Description);
        restaurantDto?.Category.Should().Be(restaurant.Category);
        restaurantDto?.HasDelivery.Should().Be(restaurant.HasDelivery);
    }
}