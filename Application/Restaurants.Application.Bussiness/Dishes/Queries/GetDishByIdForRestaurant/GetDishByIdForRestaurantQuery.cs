using MediatR;

namespace Restaurants.Application.Bussiness;

public class GetDishByIdForRestaurantQuery(int restaurantId, int dishId) : IRequest<ReadDishDto>
{
    public int RestaurantId { get; set; } = restaurantId;
    public int DishId { get; set; } = dishId;
}