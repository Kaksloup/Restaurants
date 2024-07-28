using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Bussiness.Commons;
using Restaurants.Application.Bussiness.UploadRestaurantLogo;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Services;

namespace Restaurants.Application.Bussiness;

public class RestaurantService(
    IRestaurantsRepository restaurantsRepository,
    ILogger<RestaurantService> logger,
    IMapper mapper,
    IHttpContextUserService contextUserService,
    ICustomAuthorizationService authorizationService,
    IBlobStorageService blobStorageService) : IRestaurantService
{
    public async Task<int> CreateRestaurantAsync(CreateRestaurantCommand restaurantCommand)
    {
        logger.LogInformation("Création d'un restaurant");
        var currentUser = contextUserService.GetCurrentUser();
        var restaurantToAdd = mapper.Map<Restaurant>(restaurantCommand);
        restaurantToAdd.OwnerId = currentUser?.Id;
        return await restaurantsRepository.CreateRestaurantAsync(restaurantToAdd);
    }

    public async Task<PageResult<ReadRestaurantDto>> GetRestaurantsAsync(GetRestaurantsQuery query)
    {
        logger.LogInformation("Recupération de la liste des restaurants");
        var (restaurants, total) = await restaurantsRepository.GetRestaurantsAsync(
            query.SearchPhrase,
            query.PageSize,
            query.PageNumber,
            query.SortingDirections
        );

        var items = mapper.Map<List<ReadRestaurantDto>>(restaurants);

        return new PageResult<ReadRestaurantDto>(items, total, query.PageSize, query.PageNumber);
    }

    public async Task<ReadRestaurantDto> GetRestaurantByIdAsync(int restaurantId)
    {
        logger.LogInformation("Recupération d'un restaurant");
        var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(restaurantId)
                         ?? throw new NotFoundException(nameof(Restaurant), restaurantId);

        var restaurantDto = mapper.Map<ReadRestaurantDto>(restaurant);
        restaurantDto.LogoSasUrl = blobStorageService.GetBlobSasUrl(restaurant.LogoUrl);

        return restaurantDto;
    }

    public async Task<bool> DeleteRestaurantByIdAsync(int restaurantId)
    {
        logger.LogInformation("Suppression d'un restaurant");
        var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(restaurantId)
                         ?? throw new NotFoundException(nameof(Restaurant), restaurantId);

        if (!authorizationService.Authorize(restaurant, RessourceOperation.Delete))
            throw new ForbidenException(RessourceOperation.Delete.ToString(), restaurant.Id);

        await restaurantsRepository.DeleteRestaurantAsync(restaurant);
        return true;
    }

    public async Task<bool> UpdateRestaurantAsync(UpdateRestaurantCommand restaurantCommand)
    {
        logger.LogInformation("Modification d'un restaurant");
        var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(restaurantCommand.Id)
                         ?? throw new NotFoundException(nameof(Restaurant), restaurantCommand.Id);

        if (!authorizationService.Authorize(restaurant, RessourceOperation.Update))
            throw new ForbidenException(RessourceOperation.Update.ToString(), restaurant.Id);

        var restaurantToUpdate = mapper.Map(restaurantCommand, restaurant);

        await restaurantsRepository.UpdateRestaurantAsync(restaurantToUpdate);
        return true;
    }

    public async Task<bool> UploadRestaurantLogoAsync(UploadRestaurantLogoCommand restaurantCommand)
    {
        logger.LogInformation("Chargement du logo du restaurant Id : {RestaurantId}", restaurantCommand.RestaurantId);

        var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(restaurantCommand.RestaurantId)
                         ?? throw new NotFoundException(nameof(Restaurant), restaurantCommand.RestaurantId);

        if (!authorizationService.Authorize(restaurant, RessourceOperation.Update))
            throw new ForbidenException(RessourceOperation.Update.ToString(), restaurant.Id);

        // Chargement du logo 
        var logoUri = await blobStorageService.UploadBlobAsync(restaurantCommand.File, restaurantCommand.FileName);
        restaurant.LogoUrl = logoUri;

        await restaurantsRepository.UpdateRestaurantAsync(restaurant);

        return true;
    }
}