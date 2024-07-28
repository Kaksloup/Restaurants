using MediatR;

namespace Restaurants.Application.Bussiness;

public class GetDishByIdForRestaurantQueryHandler(IDishService dishService)
    : IRequestHandler<GetDishByIdForRestaurantQuery, ReadDishDto>
{
    public async Task<ReadDishDto> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
    {
        return await dishService.GetDishByIdAndRestaurantIdAsync(request.DishId, request.RestaurantId);
    }
}