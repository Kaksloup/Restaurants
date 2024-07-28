using MediatR;

namespace Restaurants.Application.Bussiness;

public class UpdateRestaurantCommandHandler(IRestaurantService restaurantService)
    : IRequestHandler<UpdateRestaurantCommand>
{
    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        await restaurantService.UpdateRestaurantAsync(request);
    }
}