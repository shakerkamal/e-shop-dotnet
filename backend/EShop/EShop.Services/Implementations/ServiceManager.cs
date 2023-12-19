using EShop.Contracts;
using EShop.LoggerService;
using EShop.Services.Contracts;

namespace EShop.Services.Implementations;

public class ServiceManager : IServiceManager
{
    private Lazy<IProductService> _productService;
    private Lazy<IOrderService> _orderService;
    private Lazy<IUserService> _userService;

    public ServiceManager(IRepositoryManager repositoryManager,
                            ILoggerManager loggerManager)
    {
        _productService = new Lazy<IProductService>(() => new ProductService(repositoryManager, loggerManager));
        _orderService = new Lazy<IOrderService>(() => new OrderService(repositoryManager, loggerManager));
        _userService = new Lazy<IUserService>(() => new UserService(repositoryManager, loggerManager));
    }
    public IProductService ProductService => _productService.Value;

    public IOrderService OrderService => _orderService.Value;

    public IUserService UserService => _userService.Value;
}
