using MediatR;

namespace Restaurants.Application.Bussiness;

public class DeleteDishesForRestaurantCommand(int restaurantId) : IRequest
{
    public int RestaurantId { get; set; } = restaurantId;
}