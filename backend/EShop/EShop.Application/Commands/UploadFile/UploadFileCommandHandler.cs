using EShop.Services.Contracts;
using MediatR;

namespace EShop.Application.Commands.UploadFile;

internal sealed class UploadFileCommandHandler : IRequestHandler<UploadFileCommand>
{
    private readonly IServiceManager _serviceManager;

    public UploadFileCommandHandler(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public async Task Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        await _serviceManager.DocumentService.UploadAsync(request.File);
    }
}
