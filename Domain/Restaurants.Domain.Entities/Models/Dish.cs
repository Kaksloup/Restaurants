namespace Restaurants.Domain.Entities;

public class Dish
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public int RestaurantId { get; set; }
    public int? KiloCalories { get; set; }
}