namespace Restaurants.Application.Bussiness;

public class RestaurantCommand
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Category { get; set; }
    public required bool HasDelivery { get; set; }
}