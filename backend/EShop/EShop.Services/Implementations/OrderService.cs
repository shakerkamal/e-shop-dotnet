using AutoMapper;
using EShop.Contracts;
using EShop.LoggerService;
using EShop.Services.Contracts;
using EShop.Shared.DataTransferObjects.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;
    private readonly IMapper _mapper;

    public OrderService(IRepositoryManager repositoryManager, 
                        ILoggerManager loggerManager,
                        IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _loggerManager = loggerManager;
        _mapper = mapper;
    }
    public Task<OrderIndexDto> CreateOrderAsync(CreateOrderDto order)
    {
        throw new NotImplementedException();
    }

    public Task<(IEnumerable<OrderIndexDto> orders, string ids)> CreateOrderCollectionAsync(IEnumerable<CreateOrderDto> orderCollection)
    {
        throw new NotImplementedException();
    }

    public Task DeleteOrderAsync(Guid orderId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OrderIndexDto>> GetAllOrdersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OrderIndexDto>> GetByIdsAsync(IEnumerable<Guid> ids)
    {
        throw new NotImplementedException();
    }

    public Task<OrderIndexDto> GetOrderAsync(Guid orderId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateOrderAsync(Guid orderid, UpdateOrderDto orderForUpdate)
    {
        throw new NotImplementedException();
    }
}
