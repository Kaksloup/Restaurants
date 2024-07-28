using MediatR;

namespace Restaurants.Application.Bussiness;

public class GetRestaurantByIdQuery(int id) : IRequest<ReadRestaurantDto>
{
    public int Id { get; set; } = id;
}