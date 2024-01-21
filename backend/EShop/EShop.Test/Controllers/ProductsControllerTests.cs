using EShop.Api.Controllers;
using EShop.Application.Commands.CreateProduct;
using EShop.Application.Commands.DeleteProduct;
using EShop.Application.Commands.UpdateProduct;
using EShop.Application.Queries.GetProduct;
using EShop.Application.Queries.GetProducts;
using EShop.Contracts;
using EShop.Shared.DataTransferObjects.ProductDtos;
using EShop.Shared.RequestFeatures;
using MediatR;
using Microsoft.AspNetCore.Http;
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
    public async Task GetProducts_WhenCalled_ReturnsPagedListOfProducts()
    {
        // Arrange
        var pagedProduct = new PagedProduct
        {
            PageNumber = 1,
            PageSize = 10
        };

        _senderMock.Setup(x => x.Send(It.IsAny<GetProductsQuery>(), default))
                  .ReturnsAsync(new PaginatedList<ProductIndexDto>(5, 1, 5, new List<ProductIndexDto>()
                  {
                      new ProductIndexDto("1", "Xbox", string.Empty, "Microsoft", "Gaming console", 200, 450, 12),
                      new ProductIndexDto("2", "Xbox2", string.Empty, "Microsoft", "Gaming console", 100, 650, 32),
                      new ProductIndexDto("3", "Xbox3", string.Empty, "Microsoft", "Gaming console", 200, 450, 12),
                      new ProductIndexDto("4", "Xbox4", string.Empty, "Microsoft", "Gaming console", 100, 650, 32),
                      new ProductIndexDto("5", "Xbox5", string.Empty, "Microsoft", "Gaming console", 200, 450, 12),
                  }));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        // Act
        var result = await _controller.GetProducts(pagedProduct);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        _senderMock.Verify(x => x.Send(It.IsAny<GetProductsQuery>(), default), Times.Once);
    }

    [Fact]
    public async Task GetProduct_WhenCalled_ReturnsOneProduct()
    {
        // Arrange
        var productId = "1";

        _senderMock.Setup(x => x.Send(It.IsAny<GetProductDetailsQuery>(), It.IsAny<CancellationToken>()))
                  .ReturnsAsync(_productDetailsDto);

        // Act
        var result = await _controller.GetProduct(productId);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        _senderMock.Verify(x => x.Send(It.IsAny<GetProductDetailsQuery>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CreateProduct_ReturnsOkResult()
    {
        // Arrange
        var productDto = new CreateProductDto("User1", "Xbox", string.Empty, "Microsoft", "Gaming console", "Gaming console for playinh games", null, 3.9, 200, 450, 12);

        _senderMock.Setup(x => x.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>()))
                  .ReturnsAsync(new ProductIndexDto("1", "Xbox", string.Empty, "Microsoft", "Gaming console", 200, 450, 12));

        // Act
        var result = await _controller.CreateProduct(productDto);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        _senderMock.Verify(x => x.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateProduct_ReturnsNoContentResult()
    {
        // Arrange
        var productId = "1";
        var updateProduct = new UpdateProductDto("User1", "Xbox", string.Empty, "Microsoft", "Gaming console", "Gaming console for playinh games", null, 3.9, 200, 450, 12);

        _senderMock.Setup(x => x.Send(It.IsAny<UpdateProductCommand>(), It.IsAny<CancellationToken>()))
                  .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.UpdateProduct(productId, updateProduct);

        // Assert
        Assert.IsType<NoContentResult>(result);
        _senderMock.Verify(x => x.Send(It.IsAny<UpdateProductCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteProduct_ReturnsNoContentResult()
    {
        // Arrange
        var productId = "1";

        _senderMock.Setup(x => x.Send(It.IsAny<DeleteProductCommand>(), It.IsAny<CancellationToken>()))
                  .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteProduct(productId);

        // Assert
        Assert.IsType<NoContentResult>(result);
        _senderMock.Verify(x => x.Send(It.IsAny<DeleteProductCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
