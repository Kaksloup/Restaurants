using MediatR;

namespace Restaurants.Application.Bussiness;

public class DeleteDishesForRestaurantCommandHandler(IDishService dishService)
    : IRequestHandler<DeleteDishesForRestaurantCommand>
{
    public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
    {
        await dishService.DeleteDishesByRestaurantIdAsync(request.RestaurantId);
    }
}