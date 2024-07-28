using MediatR;

namespace Restaurants.Application.Bussiness;

public class UpdateRestaurantCommand : RestaurantCommand, IRequest
{
    public int Id { get; set; }
}