using MediatR;
using Restaurants.Application.Bussiness.Commons;

namespace Restaurants.Application.Bussiness;

public class GetRestaurantsQueryHandler(IRestaurantService restaurantService)
    : IRequestHandler<GetRestaurantsQuery, PageResult<ReadRestaurantDto>>
{
    public async Task<PageResult<ReadRestaurantDto>> Handle(GetRestaurantsQuery request,
        CancellationToken cancellationToken)
    {
        return await restaurantService.GetRestaurantsAsync(request);
    }
}