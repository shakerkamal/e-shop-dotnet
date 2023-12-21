using EShop.Contracts;
using EShop.Shared.DataTransferObjects.ProductDtos;
using EShop.Shared.RequestFeatures;
using MediatR;

namespace EShop.Application.Queries.GetProducts;

public sealed record GetProductsQuery(PagedProduct PagedProduct) : IRequest<PaginatedList<ProductIndexDto>>;
