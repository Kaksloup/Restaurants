using Restaurants.Application.Bussiness.Adresses.Dtos;

namespace Restaurants.Application.Bussiness;

public class ReadRestaurantDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Category { get; set; }
    public required bool HasDelivery { get; set; }
    public ReadAddressDto? Address { get; set; }
    public List<ReadDishDto> Dishes { get; set; } = [];
    public string? LogoSasUrl { get; set; }

}