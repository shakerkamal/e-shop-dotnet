using EShop.Shared.DataTransferObjects.ProductDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Queries.GetProduct;

public sealed record GetProductDetailsQuery(string Id) : IRequest<ProductDetailsDto>;
