using FluentValidation;

namespace Restaurants.Application.Bussiness.Commands.AssignRoleToUser;

public class AssignRoleToUserValidator : AbstractValidator<AssignRoleToUserCommand>
{
    public AssignRoleToUserValidator()
    {
        RuleFor(info => info.UserId).GreaterThan(0).WithMessage("User Id must be valid number.");
        RuleFor(info => info.RoleId).GreaterThan(0).WithMessage("Role Id must be valid number.");
    }
}