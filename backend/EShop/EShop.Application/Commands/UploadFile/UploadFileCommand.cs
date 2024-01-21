using MediatR;
using Microsoft.AspNetCore.Http;

namespace EShop.Application.Commands.UploadFile;

public sealed record UploadFileCommand(IFormFile File) : IRequest;
