namespace Restaurants.Application.Bussiness.Adresses.Dtos;

public class ReadAddressDto
{
    public int Id { get; set; }
    public required string City { get; set; }
    public required string Street { get; set; }
    public required string PostalCode { get; set; }
}