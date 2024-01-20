using Azure.Storage.Blobs;
using EShop.Contracts;
using EShop.Entities.ConfigurationModels;
using EShop.LoggerService;
using EShop.Services.Contracts;
using Microsoft.Extensions.Options;

namespace EShop.Services.Implementations;

public class ServiceManager : IServiceManager
{
    private Lazy<IProductService> _productService;
    private Lazy<IOrderService> _orderService;
    private Lazy<IUserService> _userService;
    private Lazy<IAuthenticationService> _authenticationService;
    private Lazy<IDocumentService> _documentService;

    public ServiceManager(IRepositoryManager repositoryManager,
                            ILoggerManager loggerManager,
                            IOptions<JwtConfiguration> configuration,
                            BlobServiceClient blobServiceClient)
    {
        _productService = new Lazy<IProductService>(() => new ProductService(repositoryManager, loggerManager));
        _orderService = new Lazy<IOrderService>(() => new OrderService(repositoryManager, loggerManager));
        _userService = new Lazy<IUserService>(() => new UserService(repositoryManager, loggerManager));
        _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(repositoryManager, loggerManager, configuration));
        _documentService = new Lazy<IDocumentService>(() => new DocumentService(blobServiceClient, loggerManager));
    }

    public IProductService ProductService => _productService.Value;

    public IOrderService OrderService => _orderService.Value;

    public IUserService UserService => _userService.Value;

    public IAuthenticationService AuthenticationService => _authenticationService.Value;

    public IDocumentService DocumentService => _documentService.Value;
}
