using MediatR;

namespace EShop.Application.Commands.DeleteProduct;

public sealed record DeleteProductCommand(string Id) : IRequest;
