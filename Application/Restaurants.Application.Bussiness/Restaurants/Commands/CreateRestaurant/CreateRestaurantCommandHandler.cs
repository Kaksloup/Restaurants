using MediatR;

namespace Restaurants.Application.Bussiness;

public class CreateRestaurantCommandHandler(IRestaurantService restaurantService)
    : IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        return await restaurantService.CreateRestaurantAsync(request);
    }
}