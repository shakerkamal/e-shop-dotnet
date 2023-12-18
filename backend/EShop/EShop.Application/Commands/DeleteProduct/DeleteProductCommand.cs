using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Commands.DeleteProduct;

public sealed record DeleteProductCommand(string Id) : IRequest;
