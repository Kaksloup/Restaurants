namespace Restaurants.Domain.Exceptions;

public class ForbidenException(string operationType, int ressourceIdentifier) :
    Exception($"Operation : {operationType} with id : {ressourceIdentifier} doesn't have permission")
{
}