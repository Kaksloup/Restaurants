using MediatR;

namespace Restaurants.Application.Bussiness;

public class GetDishesForRestaurantQuery(int restaurantId) : IRequest<List<ReadDishDto>>
{
    public int RestaurantId { get; set; } = restaurantId;
}