using EShop.Api.Controllers;
using EShop.Application.Queries.GetProduct;
using EShop.Shared.DataTransferObjects.ProductDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EShop.Test.Controllers;

public class ProductsControllerTests
{
    private Mock<ISender> _senderMock;
    private ProductsController _controller;
    private readonly ProductDetailsDto _productDetailsDto;

    public ProductsControllerTests()
    {
        _senderMock = new Mock<ISender>();
        _controller = new ProductsController(_senderMock.Object);
        _productDetailsDto = new ProductDetailsDto("1", "User1", "Xbox", string.Empty, "Microsoft", "Gaming console", "Gaming console for playinh games", null, 3.9, 200, 450, 12);
    }

    [Fact]
    public async Task GetProduct_WhenCalled_ReturnsOneProduct()
    {
        // Arrange
        //var senderMock = new Mock<ISender>();
        //var controller = new ProductsController(senderMock.Object);
        var productId = "1";

        _senderMock.Setup(x => x.Send(It.IsAny<GetProductDetailsQuery>(), It.IsAny<CancellationToken>()))
                  .ReturnsAsync(_productDetailsDto);

        // Act
        var result = await _controller.GetProduct(productId);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        _senderMock.Verify(x => x.Send(It.IsAny<GetProductDetailsQuery>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
