using MediatR;

namespace Restaurants.Application.Bussiness;

public class CreateRestaurantCommand : RestaurantCommand, IRequest<int>
{
    public string? ContactEmail { get; set; }
    public string? ContactNumber { get; set; }
    public required string City { get; set; }
    public required string Street { get; set; }
    public required string PostalCode { get; set; }
}