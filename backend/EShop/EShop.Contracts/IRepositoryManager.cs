using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Contracts;

public interface IRepositoryManager
{
    IProductRepository Product { get; }
    IOrderRepository Order { get; }
    Task SaveAsync();
    ValueTask DisposeAsync();
}
