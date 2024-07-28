using FluentValidation;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Bussiness;

public class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
{
    public CreateDishCommandValidator(IRestaurantsRepository restaurantsRepository)
    {
        RuleFor(dish => dish.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(dish => dish.Price).GreaterThan(0).WithMessage("Price must be a negative number.");
        RuleFor(dish => dish.KiloCalories).GreaterThan(0).WithMessage("KiloCalories must be a negative number.");
        RuleFor(dish => dish.RestaurantId)
            .Must(restaurantsRepository.CheckIfRestaurantExisteById)
            .WithMessage(dish => $"Restaurant with ID {dish.RestaurantId} does not exist.");
    }
}