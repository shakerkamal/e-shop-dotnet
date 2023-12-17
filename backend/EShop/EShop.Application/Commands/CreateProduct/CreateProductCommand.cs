using EShop.Shared.DataTransferObjects.ProductDtos;
using MediatR;

namespace EShop.Application.Commands.CreateProduct;

public sealed record CreateProductCommand(CreateProductDto ProductDto) : IRequest<ProductIndexDto>;
