using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Bussiness.Commands.UnassignRoleFromUser;

public class UnassignRoleFromUserHandler(
    ILogger<UnassignRoleFromUserHandler> logger,
    UserManager<User> userManager,
    RoleManager<Role> roleManager) : IRequestHandler<UnassignRoleFromUserCommand>
{
    public async Task Handle(UnassignRoleFromUserCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Assigner un role à un utilisateur {@Request}", request);

        var user = await userManager.FindByIdAsync(request.UserId.ToString())
                   ?? throw new NotFoundException(nameof(User), request.UserId);

        var role = await roleManager.FindByIdAsync(request.RoleId.ToString())
                   ?? throw new NotFoundException(nameof(Role), request.RoleId);

        await userManager.RemoveFromRoleAsync(user, role.Name!);
    }
}