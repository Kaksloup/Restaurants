namespace Restaurants.Infrastructure.BlobStorage.Configuration;

public class BlobStorageSettings
{
    public required string ConnectionString { get; set; }
    public required string LogosContainerName { get; set; }
    
    public required string AccountKey { get; set; }
}