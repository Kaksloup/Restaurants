using MediatR;

namespace Restaurants.Application.Bussiness.UploadRestaurantLogo;

public class UploadRestaurantLogoHandler(IRestaurantService restaurantService)
    : IRequestHandler<UploadRestaurantLogoCommand>
{
    public async Task Handle(UploadRestaurantLogoCommand request, CancellationToken cancellationToken)
    {
        await restaurantService.UploadRestaurantLogoAsync(request);
    }
}