using MediatR;
using Restaurants.Application.Bussiness.Commons;
using Restaurants.Domain.Repositories.Extensions;

namespace Restaurants.Application.Bussiness;

public class GetRestaurantsQuery : IRequest<PageResult<ReadRestaurantDto>>
{
    public string? SearchPhrase { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public Dictionary<string, SortingDirection>? SortingDirections { get; set; }
}