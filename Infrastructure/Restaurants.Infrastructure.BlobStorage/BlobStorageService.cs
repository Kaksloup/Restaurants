using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Services;
using Restaurants.Infrastructure.BlobStorage.Configuration;

namespace Restaurants.Infrastructure.BlobStorage;

public class BlobStorageService(IOptions<BlobStorageSettings> blobStorageOptions) : IBlobStorageService
{
    private readonly BlobStorageSettings _blobStorageSettings = blobStorageOptions.Value;

    public async Task<string> UploadBlobAsync(Stream file, string fileName)
    {
        var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(_blobStorageSettings.LogosContainerName);
        var blobClient = containerClient.GetBlobClient(fileName);
        await blobClient.UploadAsync(file);

        return blobClient.Uri.ToString();
    }

    public string? GetBlobSasUrl(string? blobUrl)
    {
        if (string.IsNullOrWhiteSpace(blobUrl))
            return null;

        var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionString);

        var blobName = new Uri(blobUrl).Segments.Last();
        
        var blobSaasBuilder = new BlobSasBuilder()
        {
            BlobContainerName = _blobStorageSettings.LogosContainerName,
            Resource = "b",
            StartsOn = DateTimeOffset.UtcNow,
            ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(30),
            BlobName = blobName,
        };
        
        blobSaasBuilder.SetPermissions(BlobSasPermissions.Read);
        
        var paramsSaas = blobSaasBuilder.ToSasQueryParameters(new StorageSharedKeyCredential(blobServiceClient.AccountName,
            _blobStorageSettings.AccountKey)).ToString();

        return $"{blobUrl}?{paramsSaas}";
    }
}