using EShop.Api.Controllers;
using EShop.Application.Commands.CreateOrder;
using EShop.Application.Commands.DeleteOrder;
using EShop.Application.Commands.UpdateOrder;
using EShop.Application.Queries.GetOrder;
using EShop.Application.Queries.GetOrders;
using EShop.Contracts;
using EShop.Shared.DataTransferObjects.OrderDtos;
using EShop.Shared.RequestFeatures;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EShop.Test.Controllers
{
    public class OrdersControllerTests
    {
        private Mock<ISender> _senderMock;
        private OrdersController _controller;
        private readonly OrderDetailsDto _orderDetailsDto;

        public OrdersControllerTests()
        {
            _senderMock = new Mock<ISender>();
            _controller = new OrdersController(_senderMock.Object);
            _orderDetailsDto = new OrderDetailsDto("1", null, null, "Card", null, 250, 50, 300, false, null, true, new DateTime(2024, 1, 1));
        }

        [Fact]
        public async Task GetOrders_WhenCalled_ReturnsPagedListOfOrders()
        {
            // Arrange
            var pagedOrder = new PagedOrder 
            { 
                PageNumber = 1,
                PageSize = 10
            };

            _senderMock.Setup(x => x.Send(It.IsAny<GetOrdersQuery>(), default))
                      .ReturnsAsync(new PaginatedList<OrderIndexDto>(5, 1, 5, new List<OrderIndexDto>()
                      {
                      new OrderIndexDto("1", "Xbox", 200, 50, 250, true, true),
                      new OrderIndexDto("2", "Xbox2", 100, 50, 250, true, true),
                      new OrderIndexDto("3", "Xbox3", 200, 50, 250, true, true),
                      new OrderIndexDto("4", "Xbox4", 100, 50, 250, true, true),
                      new OrderIndexDto("5", "Xbox5", 200, 50, 250, true, true),
                      }));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var result = await _controller.GetOrders(pagedOrder);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            _senderMock.Verify(x => x.Send(It.IsAny<GetOrdersQuery>(), default), Times.Once);
        }

        [Fact]
        public async Task GetOrder_WhenCalled_ReturnsOneOrder()
        {
            // Arrange
            var orderId = "1";

            _senderMock.Setup(x => x.Send(It.IsAny<GetOrderDetailsQuery>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(new OrderIndexDto("1", "Xbox", 200, 50, 250, true, true));

            // Act
            var result = await _controller.GetOrder(orderId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            _senderMock.Verify(x => x.Send(It.IsAny<GetOrderDetailsQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateOrder_ReturnsOkResult()
        {
            // Arrange
            var orderDto = new CreateOrderDto(null, null, "Card", null, 250, 50, 300, false, null, true, new DateTime(2024, 1, 1), "1");

            _senderMock.Setup(x => x.Send(It.IsAny<CreateOrderCommand>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(new OrderIndexDto("1", "Xbox", 200, 50, 250, true, true));

            // Act
            var result = await _controller.CreateOrder(orderDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            _senderMock.Verify(x => x.Send(It.IsAny<CreateOrderCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateOrder_ReturnsNoContentResult()
        {
            // Arrange
            var orderId = "1";
            var updateOrder = new UpdateOrderDto(null, null, "Card", null, 250, 50, 300, false, null, true, new DateTime(2024, 1, 1),"1");

            _senderMock.Setup(x => x.Send(It.IsAny<UpdateOrderCommand>(), It.IsAny<CancellationToken>()))
                      .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateOrder(orderId, updateOrder);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _senderMock.Verify(x => x.Send(It.IsAny<UpdateOrderCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteOrder_ReturnsNoContentResult()
        {
            // Arrange
            var orderId = "1";

            _senderMock.Setup(x => x.Send(It.IsAny<DeleteOrderCommand>(), It.IsAny<CancellationToken>()))
                      .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteOrder(orderId);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _senderMock.Verify(x => x.Send(It.IsAny<DeleteOrderCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
