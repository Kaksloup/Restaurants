using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Bussiness;

public class DishService(IDishRepository dishRepository, ILogger<DishService> logger, IMapper mapper) : IDishService
{
    public async Task<int> CreateDishAsync(CreateDishCommand command)
    {
        logger.LogInformation("Création d'un repas");
        var dishToAdd = mapper.Map<Dish>(command);
        return await dishRepository.CreateDishAsync(dishToAdd);
    }

    public async Task<List<ReadDishDto>> GetDishesByRestaurantIdAsync(int restaurantId)
    {
        logger.LogInformation("Recupération de la liste des repars");
        var dishes = await dishRepository.GetDishesByRestaurantIdAsync(restaurantId);

        return mapper.Map<List<ReadDishDto>>(dishes);
    }

    public async Task<ReadDishDto> GetDishByIdAndRestaurantIdAsync(int dishId, int restaurantId)
    {
        logger.LogInformation("Recupération d'un repas");
        var dish = await dishRepository.GetDishByIdAndRestaurantIdAsync(dishId, restaurantId)
                   ?? throw new NotFoundException(nameof(Dish), dishId);

        return mapper.Map<ReadDishDto>(dish);
    }

    public async Task<bool> DeleteDishesByRestaurantIdAsync(int restaurantId)
    {
        logger.LogInformation("Suppression des repas d'un restaurant");
        var dishes = await dishRepository.GetDishesByRestaurantIdAsync(restaurantId);
        if (dishes.Count == 0)
            throw new NotFoundException(nameof(Restaurant), restaurantId);

        await dishRepository.DeleteDishes(dishes);
        return true;
    }
}