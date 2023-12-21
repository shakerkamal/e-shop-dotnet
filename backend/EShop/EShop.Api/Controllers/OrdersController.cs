using EShop.Application.Commands.CreateOrder;
using EShop.Application.Commands.DeleteOrder;
using EShop.Application.Commands.UpdateOrder;
using EShop.Application.Queries.GetOrder;
using EShop.Application.Queries.GetOrders;
using EShop.Shared.DataTransferObjects.OrderDtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly ISender _sender;

        public OrdersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet(Name = "GetOrders")]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _sender.Send(new GetOrdersQuery());
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(string id)
        {
            var order = await _sender.Send(new GetOrderDetailsQuery(id));
            return Ok(order);
        }

        [HttpPost(Name = "CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto OrderDto)
        {
            var Order = await _sender.Send(new CreateOrderCommand(OrderDto));
            return Ok(Order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(string id, [FromBody] UpdateOrderDto updateOrder)
        {
            await _sender.Send(new UpdateOrderCommand(id, updateOrder));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(string id)
        {
            await _sender.Send(new DeleteOrderCommand(id));
            return NoContent();
        }
    }
}
