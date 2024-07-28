namespace Restaurants.Application.Bussiness;

public class ReadDishDto
{
    public required string Name { get; set; }
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public int? KiloCalories { get; set; }
}