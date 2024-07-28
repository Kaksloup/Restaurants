using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Bussiness.Commands.AssignRoleToUser;

public class AssignRoleToUserHandler(
    ILogger<AssignRoleToUserHandler> logger,
    UserManager<User> userManager,
    RoleManager<Role> roleManager) : IRequestHandler<AssignRoleToUserCommand>
{
    public async Task Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Assigner un role à un utilisateur {@Request}", request);

        var user = await userManager.FindByIdAsync(request.UserId.ToString())
                   ?? throw new NotFoundException(nameof(User), request.UserId);

        var role = await roleManager.FindByIdAsync(request.RoleId.ToString())
                   ?? throw new NotFoundException(nameof(Role), request.RoleId);

        await userManager.AddToRoleAsync(user, role.Name!);
    }
}