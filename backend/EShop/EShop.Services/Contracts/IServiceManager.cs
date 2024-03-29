﻿namespace EShop.Services.Contracts;

public interface IServiceManager
{
    IProductService ProductService { get; }
    IOrderService OrderService { get; }
    IUserService UserService { get; }
    IAuthenticationService AuthenticationService { get; }
    IDocumentService DocumentService { get; }
}
