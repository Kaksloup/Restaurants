using FluentValidation;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Bussiness;

public class DeleteDishesForRestaurantCommandValidator : AbstractValidator<DeleteDishesForRestaurantCommand>
{
    public DeleteDishesForRestaurantCommandValidator(IRestaurantsRepository restaurantsRepository)
    {
        RuleFor(dish => dish.RestaurantId)
            .Must(restaurantsRepository.CheckIfRestaurantExisteById)
            .WithMessage(dish => $"Restaurant with ID {dish.RestaurantId} does not exist.");
    }
}