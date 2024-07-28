using FluentValidation;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories.Extensions;

namespace Restaurants.Application.Bussiness;

public class GetRestaurantsQueryValidator : AbstractValidator<GetRestaurantsQuery>
{
    private readonly int[] _allowPageSizes = [5, 10, 15, 20, 50, 100];

    public GetRestaurantsQueryValidator()
    {
        RuleFor(info => info.PageNumber).GreaterThanOrEqualTo(1);

        RuleFor(info => info.PageSize)
            .GreaterThanOrEqualTo(1)
            .Must(value => _allowPageSizes.Contains(value))
            .WithMessage($"Page size must be in [{string.Join(",", _allowPageSizes)}] .");

        RuleFor(info => info.SortingDirections)
            .Custom((sortingDirections, context) =>
            {
                if (sortingDirections == null) return;

                var validPropertyNames = typeof(Restaurant).GetProperties()
                    .Select(p => p.Name)
                    .ToHashSet(StringComparer.OrdinalIgnoreCase);

                foreach (var key in sortingDirections.Keys.Where(key => !validPropertyNames.Contains(key)))
                    context.AddFailure($"Invalid sorting property: {key}");

                var validDirections = Enum.GetValues(typeof(SortingDirection)).Cast<SortingDirection>().ToHashSet();
                foreach (var value in sortingDirections.Values.Where(value => !validDirections.Contains(value)))
                    context.AddFailure(
                        $"Invalid sorting direction: {value}. Valid directions are Ascending or Descending.");
            });
    }
}