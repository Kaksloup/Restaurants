namespace Restaurants.Application.Bussiness;

public interface IDishService
{
    Task<int> CreateDishAsync(CreateDishCommand command);

    Task<List<ReadDishDto>> GetDishesByRestaurantIdAsync(int restaurantId);

    Task<ReadDishDto> GetDishByIdAndRestaurantIdAsync(int dishId, int restaurantId);

    Task<bool> DeleteDishesByRestaurantIdAsync(int restaurantId);
}