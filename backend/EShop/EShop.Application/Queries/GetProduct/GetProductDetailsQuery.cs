using EShop.Shared.DataTransferObjects.ProductDtos;
using MediatR;

namespace EShop.Application.Queries.GetProduct;

public sealed record GetProductDetailsQuery(string Id) : IRequest<ProductDetailsDto>;
