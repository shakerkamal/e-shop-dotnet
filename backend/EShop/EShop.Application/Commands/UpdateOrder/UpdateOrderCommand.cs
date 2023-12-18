using EShop.Shared.DataTransferObjects.OrderDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Commands.UpdateOrder;

public sealed record UpdateOrderCommand(string Id, UpdateOrderDto UpdateOrder) : IRequest;
