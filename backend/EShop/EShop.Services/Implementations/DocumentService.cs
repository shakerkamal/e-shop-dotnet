using Azure.Storage.Blobs;
using EShop.LoggerService;
using EShop.Services.Contracts;
using EShop.Shared.DataTransferObjects.DocumentDtos;
using Microsoft.AspNetCore.Http;

namespace EShop.Services.Implementations;

public class DocumentService : IDocumentService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly ILoggerManager _logger;

    //To do: Must remove this container name and read it from database.
    private const string blobContainerName = "product-images";

    public DocumentService(BlobServiceClient blobServiceClient, ILoggerManager logger)
    {
        _blobServiceClient = blobServiceClient;
        _logger = logger;
    }

    public async Task UploadAsync(IFormFile file)
    {

        var fileType = Path.GetExtension(file.FileName);
        if ((fileType.ToLower() == ".jpg" || fileType.ToLower() == ".jpeg" || fileType.ToLower() == ".png") || file.Length == 0)
        {
            using (var stream = file.OpenReadStream())
            {
                await UploadContentAsync(file.Name, stream);
            }
        }
    }

    public async Task<FileInfoDto?> DownloadAsync(string fileName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(blobContainerName);
        containerClient.CreateIfNotExists();

        var file = containerClient.GetBlobClient(fileName);
        if (await file.ExistsAsync())
        {
            var downloadInfo = await file.DownloadAsync();
            return new FileInfoDto(
                        file.Uri.AbsoluteUri,
                        fileName,
                        downloadInfo.Value.Details.ContentType,
                        downloadInfo.Value.Content);
        }
        return null;
    }

    public async Task DeleteAsync(string fileName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(blobContainerName);
        var file = containerClient.GetBlobClient(fileName);
        await file.DeleteAsync();
    }

    #region private methods
    private async Task<string> UploadContentAsync(string fileName, Stream content)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(blobContainerName);
        var blobClient = containerClient.GetBlobClient(fileName);
        if (await blobClient.ExistsAsync())
        {
            //To do: log file exist
            _logger.LogError($"{fileName} already exist in storage. Provide a unique file name.");
            return string.Empty;
        }
        else
        {
            var response = await blobClient.UploadAsync(content);
            return response.Value.EncryptionScope;
        }
    }

    #endregion
}
