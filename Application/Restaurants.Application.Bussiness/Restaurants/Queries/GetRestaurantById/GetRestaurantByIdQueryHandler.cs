using MediatR;

namespace Restaurants.Application.Bussiness;

public class GetRestaurantByIdQueryHandler(IRestaurantService restaurantService)
    : IRequestHandler<GetRestaurantByIdQuery, ReadRestaurantDto?>
{
    public async Task<ReadRestaurantDto?> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        return await restaurantService.GetRestaurantByIdAsync(request.Id);
    }
}