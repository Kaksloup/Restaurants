using AutoMapper;
using FluentAssertions;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Bussiness;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Entities.Constants;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Services;

namespace Restaurants.Tests.Application.Restaurants;

[TestSubject(typeof(RestaurantService))]
public class RestaurantServiceTest
{
    private static readonly CreateRestaurantCommand CreateCommand = new()
    {
        City = null,
        Street = null,
        PostalCode = null,
        Name = "Test restaurant",
        Description = "Ceci est un test restaurant",
        Category = "Test",
        HasDelivery = true
    };

    private static readonly Restaurant RestaurantCreated = new()
    {
        Name = CreateCommand.Name,
        Description = CreateCommand.Description,
        Category = CreateCommand.Category,
        HasDelivery = CreateCommand.HasDelivery
    };

    private static readonly UpdateRestaurantCommand UpdateCommand = new()
    {
        Id = 1,
        Name = CreateCommand.Name + 'U',
        Description = CreateCommand.Description + 'U',
        Category = CreateCommand.Category + 'U',
        HasDelivery = CreateCommand.HasDelivery
    };

    private static readonly Restaurant RestaurantUpdated = new()
    {
        Id = 1,
        Name = CreateCommand.Name,
        Description = CreateCommand.Description,
        Category = CreateCommand.Category,
        HasDelivery = CreateCommand.HasDelivery
    };

    private readonly Mock<ICustomAuthorizationService> _authorizationServiceMock;
    private readonly Mock<IBlobStorageService> _blobStorageService;
    private readonly Mock<IHttpContextUserService> _contextUserServiceMock;
    private readonly Mock<ILogger<RestaurantService>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly RestaurantService _restaurantServiceMock;
    private readonly Mock<IRestaurantsRepository> _restaurantsRepositoryMock;

    public RestaurantServiceTest()
    {
        _loggerMock = new Mock<ILogger<RestaurantService>>();
        _authorizationServiceMock = new Mock<ICustomAuthorizationService>();

        _restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();

        _contextUserServiceMock = new Mock<IHttpContextUserService>();

        _blobStorageService = new Mock<IBlobStorageService>();

        _contextUserServiceMock.Setup(context => context.GetCurrentUser())
            .Returns(new CurrentUserDto(1, "test@restaurant.com", [UtilRoles.Administrator, UtilRoles.User],
                DateOnly.MaxValue));


        _mapperMock = new Mock<IMapper>();

        _restaurantServiceMock = new RestaurantService(_restaurantsRepositoryMock.Object,
            _loggerMock.Object, _mapperMock.Object, _contextUserServiceMock.Object, _authorizationServiceMock.Object,
            _blobStorageService.Object);
    }

    [Fact]
    public async Task Handle_ForValidCommand_ReturnCreateRestaurantId()
    {
        // Arrange
        _mapperMock.Setup(m => m.Map<Restaurant>(CreateCommand)).Returns(RestaurantCreated);
        _authorizationServiceMock.Setup(auth => auth.Authorize(RestaurantCreated, RessourceOperation.Update))
            .Returns(true);
        _restaurantsRepositoryMock.Setup(repo => repo.CreateRestaurantAsync(It.IsAny<Restaurant>()))
            .ReturnsAsync(1);

        // Act
        var result = await _restaurantServiceMock.CreateRestaurantAsync(CreateCommand);

        // Assert
        result.Should().Be(1);
        RestaurantCreated.OwnerId.Should().Be(1);
        _restaurantsRepositoryMock.Verify(r => r.CreateRestaurantAsync(RestaurantCreated), Times.Once);
    }

    [Fact]
    public async Task Handle_ForValidCommand_ReturnUpdateRestaurantId()
    {
        // Arrange
        _restaurantsRepositoryMock.Setup(repo => repo.GetRestaurantByIdAsync(UpdateCommand.Id))
            .ReturnsAsync(RestaurantUpdated);
        _authorizationServiceMock.Setup(auth => auth.Authorize(RestaurantUpdated, RessourceOperation.Update))
            .Returns(true);

        // Act
        var result = await _restaurantServiceMock.UpdateRestaurantAsync(UpdateCommand);

        // Assert
        result.Should().Be(true);
        _restaurantsRepositoryMock.Verify(repo => repo.UpdateRestaurantAsync(It.IsAny<Restaurant>()), Times.Once);
        _mapperMock.Verify(map => map.Map(UpdateCommand, RestaurantUpdated), Times.Once);
    }

    [Fact]
    public async Task Handle_WithNonExistingRestaurant_ShouldThrowNotFoundException()
    {
        // Arrange
        _restaurantsRepositoryMock.Setup(repo => repo.GetRestaurantByIdAsync(UpdateCommand.Id))
            .ReturnsAsync((Restaurant?)null);

        // Act
        Func<Task<bool>> action = async () => await _restaurantServiceMock.UpdateRestaurantAsync(UpdateCommand);

        // Assert
        await action.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Restaurant with id : {UpdateCommand.Id} doesn't existe");
    }

    [Fact]
    public async Task Handle_WithUnauthorizedUser_ShouldThrowForbidenException()
    {
        // Arrange
        _restaurantsRepositoryMock.Setup(repo => repo.GetRestaurantByIdAsync(UpdateCommand.Id))
            .ReturnsAsync(RestaurantUpdated);
        _authorizationServiceMock.Setup(auth => auth.Authorize(RestaurantUpdated, RessourceOperation.Update))
            .Returns(false);

        // Act
        Func<Task<bool>> action = async () => await _restaurantServiceMock.UpdateRestaurantAsync(UpdateCommand);

        // Assert
        await action.Should().ThrowAsync<ForbidenException>()
            .WithMessage($"Operation : Update with id : {UpdateCommand.Id} doesn't have permission");
    }
}