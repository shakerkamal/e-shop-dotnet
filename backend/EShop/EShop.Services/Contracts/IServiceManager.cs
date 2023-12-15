using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Services.Contracts;

public interface IServiceManager
{
    IProductService ProductService { get; }
    IOrderService OrderService { get; }
}
