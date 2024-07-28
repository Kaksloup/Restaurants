using MediatR;

namespace Restaurants.Application.Bussiness;

public class DeleteRestaurantCommandHandler(IRestaurantService restaurantService)
    : IRequestHandler<DeleteRestaurantCommand>
{
    public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        await restaurantService.DeleteRestaurantByIdAsync(request.Id);
    }
}