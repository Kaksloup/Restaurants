using MediatR;

namespace Restaurants.Application.Bussiness;

public class GetDishesForRestaurantQueryHandler(IDishService dishService)
    : IRequestHandler<GetDishesForRestaurantQuery, List<ReadDishDto>>
{
    public Task<List<ReadDishDto>> Handle(GetDishesForRestaurantQuery request, CancellationToken cancellationToken)
    {
        return dishService.GetDishesByRestaurantIdAsync(request.RestaurantId);
    }
}