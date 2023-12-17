using AutoMapper;
using EShop.Contracts;
using EShop.Entities.Exceptions;
using EShop.Entities.Models;
using EShop.LoggerService;
using EShop.Services.Contracts;
using EShop.Shared.DataTransferObjects.ProductDtos;
using System.Collections;

namespace EShop.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;
    private readonly IMapper _mapper;

    public ProductService(IRepositoryManager repositoryManager, 
                        ILoggerManager loggerManager,
                        IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _loggerManager = loggerManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductIndexDto>> GetAllProductsAsync()
    {
        var products = await _repositoryManager.Product.GetAllAsync();

        var response = ConvertToProductIndexDto(products);
        return response;
    }

    public async Task<ProductIndexDto> GetProductAsync(string productId)
    {
        if (string.IsNullOrEmpty(productId))
        {
            throw new ArgumentNullException();
        }
        var product = await _repositoryManager.Product.GetAsync(productId);
        if (product == null)
        {
            throw new ProductNotFoundException();
        }

        return new ProductIndexDto(
            product.Name,
            product.Image,
            product.Brand,
            product.Category,
            product.NumReviews,
            product.Price,
            product.Rating);
    }
    public async Task<ProductIndexDto> CreateProductAsync(CreateProductDto product)
    {
        var productDocument = MapProductDtoToDocument(product);

        await _repositoryManager.Product.AddAsync(productDocument);

        return new ProductIndexDto(
            productDocument.Name, 
            productDocument.Image, 
            productDocument.Brand, 
            productDocument.Category, 
            productDocument.NumReviews, 
            productDocument.Price, 
            productDocument.Rating);
    }

    public Task<(IEnumerable<ProductIndexDto> products, string ids)> CreateProductCollectionAsync(IEnumerable<CreateProductDto> productCollection)
    {
        throw new NotImplementedException();
    }

    public Task DeleteProductAsync(string productId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductIndexDto>> GetByIdsAsync(IEnumerable<string> ids)
    {
        throw new NotImplementedException();
    }

    public Task UpdateProductAsync(string productid, UpdateProductDto productForUpdate)
    {
        throw new NotImplementedException();
    }

    #region private methods

    private Product MapProductDtoToDocument(CreateProductDto product)
    {
        var productDocument = new Product
        {
            Name = product.Name,
            Image = product.Image,
            Brand = product.Brand,
            Description = product.Description,
            Category = product.Category,
            NumReviews = product.NumReviews,
            CountInStock = product.CountInStock,
            Price = product.Price,
            Rating = product.Rating,
            UserId = product.UserId,
        };

        if (product.NumReviews > 0)
        {
            productDocument.Reviews = MapProductReviews(product.Reviews);
        }

        return productDocument;
    }


    private List<ProductIndexDto> ConvertToProductIndexDto(IEnumerable<Product> products)
    {
        var productsDto = new List<ProductIndexDto>();  
        foreach (var product in products)
        {
            var productDto = new ProductIndexDto(
                product.Name,
                product.Image,
                product.Brand,
                product.Category,
                product.NumReviews,
                product.Price,
                product.Rating);
            productsDto.Add(productDto);
        }

        return productsDto;
    }

    private List<Entities.Models.Review>? MapProductReviews(List<Shared.DataTransferObjects.ProductDtos.Review>? reviews)
    {
        if (reviews is not null)
        {
            var reviewDocuments = new List<Entities.Models.Review>();

            foreach (var review in reviews)
            {
                var reviewDocument = new Entities.Models.Review
                {
                    Name=review.Name,
                    Comment = review.Comment,
                    Rating =review.Rating
                };

                reviewDocuments.Add(reviewDocument);
            }
        }
        return Enumerable.Empty<Entities.Models.Review>().ToList();
    }

    #endregion
}
