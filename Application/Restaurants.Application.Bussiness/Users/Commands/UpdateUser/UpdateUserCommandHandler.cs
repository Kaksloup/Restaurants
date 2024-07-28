using MediatR;

namespace Restaurants.Application.Bussiness;

public class UpdateUserCommandHandler(IUserService userService) : IRequestHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        await userService.UpdateUserAsync(request, cancellationToken);
    }
}