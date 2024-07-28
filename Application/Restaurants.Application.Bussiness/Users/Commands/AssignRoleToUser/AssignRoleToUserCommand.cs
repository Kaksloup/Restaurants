using MediatR;

namespace Restaurants.Application.Bussiness.Commands.AssignRoleToUser;

public class AssignRoleToUserCommand : IRequest
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
}