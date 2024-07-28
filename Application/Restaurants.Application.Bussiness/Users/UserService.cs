using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Bussiness;

public class UserService(
    ILogger<UserService> logger,
    IHttpContextUserService httpContextUser,
    IUserStore<User> userStore,
    IMapper mapper) : IUserService
{
    public async Task UpdateUserAsync(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var currentUser = httpContextUser.GetCurrentUser();
        logger.LogInformation(
            "Mise à jour des informations de l'utilisateur [{UserId} avec la commande {@Command}] par {CurrentUserId}",
            command.UserId, command, currentUser!.Id);
        var dbUser = await userStore.FindByIdAsync(command.UserId.ToString(), cancellationToken) ??
                     throw new NotFoundException(nameof(User), command.UserId);

        var userToUpdate = mapper.Map(command, dbUser);

        await userStore.UpdateAsync(userToUpdate, cancellationToken);
    }
}