using FluentValidation;

namespace Restaurants.Application.Bussiness;

public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateRestaurantCommand>
{
    public UpdateRestaurantCommandValidator()
    {
        RuleFor(dto => dto.Name).Length(3, 400);
        RuleFor(dto => dto.Description).NotEmpty().WithMessage("Description is required");
    }
}