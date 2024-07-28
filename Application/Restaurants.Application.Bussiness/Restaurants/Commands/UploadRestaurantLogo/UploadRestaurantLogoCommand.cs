using MediatR;

namespace Restaurants.Application.Bussiness.UploadRestaurantLogo;

public class UploadRestaurantLogoCommand : IRequest
{
    public int RestaurantId { get; set; }
    public required string FileName { get; set; }
    public required Stream File { get; set; }
}