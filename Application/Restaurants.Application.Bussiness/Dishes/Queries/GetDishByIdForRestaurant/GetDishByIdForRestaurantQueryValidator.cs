using FluentValidation;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Bussiness;

public class GetDishByIdForRestaurantQueryValidator : AbstractValidator<GetDishByIdForRestaurantQuery>
{
    public GetDishByIdForRestaurantQueryValidator(IRestaurantsRepository restaurantsRepository,
        IDishRepository dishRepository)
    {
        RuleFor(dish => dish.RestaurantId)
            .Must(restaurantsRepository.CheckIfRestaurantExisteById)
            .WithMessage(dish => $"Restaurant with ID {dish.RestaurantId} does not exist.");

        RuleFor(dish => dish.DishId)
            .Must(dishRepository.CheckIfDishExisteById)
            .WithMessage(dish => $"Dish with ID {dish.DishId} does not exist.");
    }
}