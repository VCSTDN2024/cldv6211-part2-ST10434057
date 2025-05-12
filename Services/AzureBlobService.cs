namespace WebAppPart1ST10434057.Services;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

public class AzureBlobService
{
    private readonly string _sasUrl;
    private readonly BlobContainerClient _containerClient;

    public AzureBlobService(IConfiguration configuration)
    {
        _sasUrl = configuration["AzureBlobStorage:SasUrl"];
        _containerClient = new BlobContainerClient(new Uri(_sasUrl));
    }

    public async Task<string> UploadImageAsync(Stream fileStream, string fileName)
    {
        BlobClient blobClient = _containerClient.GetBlobClient(fileName);
        await blobClient.UploadAsync(fileStream, overwrite: true);
        return blobClient.Uri.ToString();
    }
}
