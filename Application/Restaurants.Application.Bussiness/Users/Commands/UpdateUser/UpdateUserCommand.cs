using MediatR;

namespace Restaurants.Application.Bussiness;

public class UpdateUserCommand : IRequest
{
    public int UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateOnly? BirthDate { get; set; }
}