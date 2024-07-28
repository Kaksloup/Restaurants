using MediatR;

namespace Restaurants.Application.Bussiness;

public class CreateDishCommandHandler(IDishService dishService) : IRequestHandler<CreateDishCommand, int>
{
    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        return await dishService.CreateDishAsync(request);
    }
}