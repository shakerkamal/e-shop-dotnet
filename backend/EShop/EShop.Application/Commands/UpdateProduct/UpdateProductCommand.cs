using EShop.Shared.DataTransferObjects.ProductDtos;
using MediatR;

namespace EShop.Application.Commands.UpdateProduct;

public sealed record UpdateProductCommand(string Id, UpdateProductDto UpdateProduct) : IRequest;
