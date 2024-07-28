using AutoMapper;
using FluentAssertions;
using JetBrains.Annotations;
using Restaurants.Application.Bussiness;
using Restaurants.Application.Bussiness.Adresses;
using Restaurants.Domain.Entities;

namespace Restaurants.Tests.Application.Restaurants;

[TestSubject(typeof(RestaurantProfile))]
public class RestaurantProfileTest
{
    private readonly IMapper _mapper;

    public RestaurantProfileTest()
    {
        var configuration = new MapperConfiguration(c =>
        {
            c.AddProfile<DishProfile>();
            c.AddProfile<AddressProfile>();
            c.AddProfile<RestaurantProfile>();
        });

        _mapper = configuration.CreateMapper();
    }

    [Fact]
    public void CreateMap_ForRestaurantToRestaurantDto_MapCorrectly()
    {
        // Arrange
        var restaurant = new Restaurant
        {
            Id = 1,
            Name = "Test restaurant",
            Description = "Ceci est un exemple de test",
            Category = "Test",
            HasDelivery = true,
            ContactEmail = "test@restaurant.com",
            ContactNumber = "07778889962",
            Address = new Address
            {
                Id = 1,
                City = "Lyon",
                Street = "Rue des vaillants",
                PostalCode = "69-000"
            },
            Dishes =
            [
                new Dish
                {
                    Name = "repas test du restaurant"
                }
            ]
        };
        // Act
        var restaurantDto = _mapper.Map<ReadRestaurantDto>(restaurant);
        // Assert
        restaurantDto.Should().NotBeNull();
        restaurantDto.Id.Should().Be(restaurant.Id);
        restaurantDto.Category.Should().Be(restaurant.Category);
        restaurantDto.Address.Should().NotBeNull()
            .And.BeEquivalentTo(restaurant.Address);
        restaurantDto.Dishes.Should().NotBeEmpty()
            .And.HaveCount(1)
            .And.Satisfy(d => d.Name == "repas test du restaurant");
    }

    [Fact]
    public void CreateMap_ForCreateRestaurantCommandToRestaurant_MapCorrectly()
    {
        // Arrange
        var createRestaurantCommand = new CreateRestaurantCommand
        {
            Name = "Test restaurant",
            Description = "Ceci est un exemple de test",
            Category = "Test",
            HasDelivery = true,
            ContactEmail = "test@restaurant.com",
            ContactNumber = "07778889962",
            City = "Lyon",
            Street = "Rue des vaillants",
            PostalCode = "69-000"
        };

        // Act
        var restaurant = _mapper.Map<Restaurant>(createRestaurantCommand);

        // Assert
        restaurant.Should().NotBeNull();
        restaurant.Category.Should().Be(createRestaurantCommand.Category);
        restaurant.Name.Should().Be(createRestaurantCommand.Name);
        restaurant.Description.Should().Be(createRestaurantCommand.Description);
        restaurant.ContactEmail.Should().Be(createRestaurantCommand.ContactEmail);
        restaurant.ContactNumber.Should().Be(createRestaurantCommand.ContactNumber);
        restaurant.HasDelivery.Should().Be(createRestaurantCommand.HasDelivery);
        restaurant.Address.Should().NotBeNull();
        restaurant.Address?.City.Should().Be(createRestaurantCommand.City);
        restaurant.Address?.Street.Should().Be(createRestaurantCommand.Street);
        restaurant.Address?.PostalCode.Should().Be(createRestaurantCommand.PostalCode);
    }

    [Fact]
    public void CreateMap_ForUpdateRestaurantCommandToRestaurant_MapCorrectly()
    {
        // Arrange
        var updateRestaurantCommand = new UpdateRestaurantCommand
        {
            Id = 1,
            Name = "Test restaurant",
            Description = "Ceci est un exemple de test",
            Category = "Test",
            HasDelivery = true
        };

        // Act
        var restaurant = _mapper.Map<Restaurant>(updateRestaurantCommand);

        // Assert
        restaurant.Should().NotBeNull();
        restaurant.Id.Should().Be(updateRestaurantCommand.Id);
        restaurant.Category.Should().Be(updateRestaurantCommand.Category);
        restaurant.Name.Should().Be(updateRestaurantCommand.Name);
        restaurant.Description.Should().Be(updateRestaurantCommand.Description);
        restaurant.HasDelivery.Should().Be(updateRestaurantCommand.HasDelivery);
    }
}