using FluentValidation;

namespace Restaurants.Application.Bussiness.Commands.UnassignRoleFromUser;

public class UnassignRoleFromUserValidator : AbstractValidator<UnassignRoleFromUserCommand>
{
    public UnassignRoleFromUserValidator()
    {
        RuleFor(info => info.UserId).GreaterThan(0).WithMessage("User Id must be valid number.");
        RuleFor(info => info.RoleId).GreaterThan(0).WithMessage("Role Id must be valid number.");
    }
}