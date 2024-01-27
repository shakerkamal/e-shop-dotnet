using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Contracts;
using EShop.Entities.Models;
using EShop.LoggerService;
using EShop.Services.Contracts;
using EShop.Services.Implementations;
using EShop.Shared.DataTransferObjects.ProductDtos;
using EShop.Shared.RequestFeatures;
using MongoDB.Bson;
using Moq;

namespace EShop.Test.Services;

public class ProductServiceTests
{
    private readonly Mock<IRepositoryManager> _repositoryManagerMock;
    private readonly Mock<ILoggerManager> _loggerManagerMock;
    private readonly IProductService _productService;

    public ProductServiceTests()
    {
        _repositoryManagerMock = new Mock<IRepositoryManager>();
        _loggerManagerMock = new Mock<ILoggerManager>();
        _productService = new ProductService(_repositoryManagerMock.Object, _loggerManagerMock.Object);
    }

    [Fact]
    public async Task GetAllProductsAsync_WhenCalled_ShouldReturnListOfProductIndexDtos()
    {
        // Arrange
        var products = GetProductsList();
        _repositoryManagerMock.Setup(repo => repo.Product.GetAllAsync(default)).ReturnsAsync(products);

        // Act
        var result = await _productService.GetAllProductsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<ProductIndexDto>>(result);
        // Add more specific assertions based on your expectations
    }

    [Fact]
    public async Task GetAllProductsAsync_WithPagedProduct_ShouldReturnPaginatedList()
    {
        // Arrange
        var pagedProduct = new PagedProduct { PageNumber = 1, PageSize = 10 };
        var paginatedList = new PaginatedList<ProductIndexDto>( 1, 10, 20, new List<ProductIndexDto>()
                  {
                      new ProductIndexDto("1", "Xbox", string.Empty, "Microsoft", "Gaming console", 200, 450, 12),
                      new ProductIndexDto("2", "Xbox2", string.Empty, "Microsoft", "Gaming console", 100, 650, 32),
                      new ProductIndexDto("3", "Xbox3", string.Empty, "Microsoft", "Gaming console", 200, 450, 12),
                      new ProductIndexDto("4", "Xbox4", string.Empty, "Microsoft", "Gaming console", 100, 650, 32),
                      new ProductIndexDto("5", "Xbox5", string.Empty, "Microsoft", "Gaming console", 200, 450, 12),
                  });

        _repositoryManagerMock.Setup(repo => repo.Product.GetAllAsync<ProductIndexDto>(It.IsAny<Func<BsonDocument, ProductIndexDto>>(), pagedProduct.PageNumber, pagedProduct.PageSize))
            .ReturnsAsync(paginatedList);

        // Act
        var result = await _productService.GetAllProductsAsync(pagedProduct);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<PaginatedList<ProductIndexDto>>(result);
        // Add more specific assertions based on your expectations
    }

    [Fact]
    public async Task GetProductAsync_WithValidProductId_ShouldReturnProductDetailsDto()
    {
        // Arrange
        var productId = "1";
        var product = new Product(); // Your sample product
        _repositoryManagerMock.Setup(repo => repo.Product.GetAsync(productId)).ReturnsAsync(product);

        // Act
        var result = await _productService.GetProductAsync(productId);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ProductDetailsDto>(result);
        // Add more specific assertions based on your expectations
    }

    [Fact]
    public async Task GetProductAsync_WithNullProductId_ShouldThrowArgumentNullException()
    {
        // Arrange
        string productId = null;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _productService.GetProductAsync(productId));
    }

    // Add more tests for other methods as needed...


    #region helper methods
    private IEnumerable<Product> GetProductsList()
    {
        return new List<Product> {
            new Product
            {
                Id = new ObjectId("1"),
                CreatedAt = DateTime.UtcNow,
                Brand = "Microsoft",
                Category = "Tab",
                CountInStock = 10,
                Description = "Description",
                Image = string.Empty,
                Name = "Surface Tab Duo",
                NumReviews = 10,
                Price = 500,
                Rating = 3.9,
                UserId = "User1"
            },
            new Product
            {
                Id = new ObjectId("1"),
                CreatedAt = DateTime.UtcNow,
                Brand = "Microsoft",
                Category = "Tab",
                CountInStock = 10,
                Description = "Description",
                Image = string.Empty,
                Name = "Surface Tab Duo",
                NumReviews = 10,
                Price = 500,
                Rating = 3.9,
                UserId = "User1"
            },
            new Product
            {
                Id = new ObjectId("1"),
                CreatedAt = DateTime.UtcNow,
                Brand = "Microsoft",
                Category = "Tab",
                CountInStock = 10,
                Description = "Description",
                Image = string.Empty,
                Name = "Surface Tab Duo",
                NumReviews = 10,
                Price = 500,
                Rating = 3.9,
                UserId = "User1"
            },
            new Product
            {
                Id = new ObjectId("1"),
                CreatedAt = DateTime.UtcNow,
                Brand = "Microsoft",
                Category = "Tab",
                CountInStock = 10,
                Description = "Description",
                Image = string.Empty,
                Name = "Surface Tab Duo",
                NumReviews = 10,
                Price = 500,
                Rating = 3.9,
                UserId = "User1"
            },
            new Product
            {
                Id = new ObjectId("1"),
                CreatedAt = DateTime.UtcNow,
                Brand = "Microsoft",
                Category = "Tab",
                CountInStock = 10,
                Description = "Description",
                Image = string.Empty,
                Name = "Surface Tab Duo",
                NumReviews = 10,
                Price = 500,
                Rating = 3.9,
                UserId = "User1"
            }
        };
    }
    #endregion
}
