namespace Restaurants.Domain.Exceptions;

public class NotFoundException(string ressourceType, int ressourceIdentifier)
    : Exception($"{ressourceType} with id : {ressourceIdentifier} doesn't existe")
{
}