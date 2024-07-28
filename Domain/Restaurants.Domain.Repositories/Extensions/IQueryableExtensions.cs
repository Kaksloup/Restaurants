using System.Linq.Expressions;

namespace Restaurants.Domain.Repositories.Extensions;

public static class IQueryableExtensions
{
    public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query,
        Dictionary<string, SortingDirection>? sortingDirections)
    {
        if (sortingDirections == null || !sortingDirections.Any())
            return query;

        IOrderedQueryable<T>? orderedQuery = null;
        foreach (var sort in sortingDirections)
        {
            var propertyName = sort.Key;
            var direction = sort.Value;

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);
            var lambda = Expression.Lambda(property, parameter);

            var methodName = direction == SortingDirection.Ascending ? "OrderBy" : "OrderByDescending";

            if (orderedQuery == null)
            {
                orderedQuery = typeof(Queryable).GetMethods().Single(
                        method => method.Name == methodName
                                  && method.IsGenericMethodDefinition
                                  && method.GetGenericArguments().Length == 2
                                  && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), property.Type)
                    .Invoke(null, new object[] { query, lambda }) as IOrderedQueryable<T>;
            }
            else
            {
                methodName = direction == SortingDirection.Ascending ? "ThenBy" : "ThenByDescending";
                orderedQuery = typeof(Queryable).GetMethods().Single(
                        method => method.Name == methodName
                                  && method.IsGenericMethodDefinition
                                  && method.GetGenericArguments().Length == 2
                                  && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), property.Type)
                    .Invoke(null, new object[] { orderedQuery, lambda }) as IOrderedQueryable<T>;
            }
        }

        return orderedQuery ?? query;
    }
}