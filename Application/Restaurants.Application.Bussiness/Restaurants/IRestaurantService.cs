using Restaurants.Application.Bussiness.Commons;
using Restaurants.Application.Bussiness.UploadRestaurantLogo;

namespace Restaurants.Application.Bussiness;

public interface IRestaurantService
{
    Task<PageResult<ReadRestaurantDto>> GetRestaurantsAsync(GetRestaurantsQuery query);
    Task<ReadRestaurantDto> GetRestaurantByIdAsync(int restaurantId);
    Task<int> CreateRestaurantAsync(CreateRestaurantCommand restaurantCommand);
    Task<bool> DeleteRestaurantByIdAsync(int restaurantId);
    Task<bool> UpdateRestaurantAsync(UpdateRestaurantCommand restaurantCommand);
    Task<bool> UploadRestaurantLogoAsync(UploadRestaurantLogoCommand restaurantCommand);
}