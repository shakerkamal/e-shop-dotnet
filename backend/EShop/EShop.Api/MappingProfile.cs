using AutoMapper;
using EShop.Entities.Models;
using EShop.Shared.DataTransferObjects.OrderDtos;
using EShop.Shared.DataTransferObjects.ProductDtos;
using EShop.Shared.DataTransferObjects.UserDtos;

namespace EShop.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductIndexDto>();
            CreateMap<Product, ProductDetailsDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();

            CreateMap<Order, OrderIndexDto>();
            CreateMap<Order, OrderDetailsDto>();
            CreateMap<CreateOrderDto, Order>();
            CreateMap<UpdateOrderDto, Order>();

            CreateMap<User, UserIndexDto>();
            CreateMap<CreateUserDto, UserIndexDto>();
        }
    }
}
