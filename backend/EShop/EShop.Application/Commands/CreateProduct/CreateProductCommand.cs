using EShop.Shared.DataTransferObjects.ProductDtos;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EShop.Application.Commands.CreateProduct;

public sealed record CreateProductCommand(CreateProductDto ProductDto) : IRequest<ProductIndexDto>;
