using MediatR;

namespace Restaurants.Application.Bussiness.Commands.UnassignRoleFromUser;

public class UnassignRoleFromUserCommand : IRequest
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
}