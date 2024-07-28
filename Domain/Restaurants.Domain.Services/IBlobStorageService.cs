namespace Restaurants.Domain.Services;

public interface IBlobStorageService
{
    Task<string> UploadBlobAsync(Stream file, string fileName);

    string? GetBlobSasUrl(string? blobUrl);
}