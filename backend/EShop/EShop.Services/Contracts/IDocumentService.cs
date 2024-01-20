using EShop.Shared.DataTransferObjects.DocumentDtos;
using Microsoft.AspNetCore.Http;

namespace EShop.Services.Contracts;

public interface IDocumentService
{
    Task UploadAsync(IFormFile file);
    Task<FileInfoDto?> DownloadAsync(string fileName);
    Task DeleteAsync(string fileName);
}