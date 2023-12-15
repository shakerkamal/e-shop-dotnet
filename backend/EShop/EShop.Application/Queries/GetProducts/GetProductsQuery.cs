using EShop.Shared.DataTransferObjects.ProductDtos;
using MediatR;

namespace EShop.Application.Queries.GetProducts;

public sealed record GetProductsQuery() : IRequest<IEnumerable<ProductIndexDto>>;
