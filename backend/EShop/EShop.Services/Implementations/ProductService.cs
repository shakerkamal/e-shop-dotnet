using AutoMapper;
using EShop.Contracts;
using EShop.Entities.Exceptions;
using EShop.Entities.Models;
using EShop.LoggerService;
using EShop.Services.Contracts;
using EShop.Shared.DataTransferObjects.ProductDtos;
using MongoDB.Bson;
using System.Collections;

namespace EShop.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;

    public ProductService(IRepositoryManager repositoryManager, 
                        ILoggerManager loggerManager)
    {
        _repositoryManager = repositoryManager;
        _loggerManager = loggerManager;
    }

    public async Task<IEnumerable<ProductIndexDto>> GetAllProductsAsync()
    {
        var products = await _repositoryManager.Product.GetAllAsync();

        var response = MapToProductIndexDtos(products);
        return response;
    }

    public async Task<ProductDetailsDto> GetProductAsync(string productId)
    {
        if (string.IsNullOrEmpty(productId))
        {
            throw new ArgumentNullException();
        }
        var product = await _repositoryManager.Product.GetAsync(productId);
        if (product == null)
        {
            throw new ProductNotFoundException(productId);
        }

        var reviews = MapReviewsToReviewDto(product.Reviews);

        return new ProductDetailsDto(
            product.Id.ToString(),
            product.UserId,
            product.Name,
            product.Image,
            product.Brand,
            product.Category,
            product.Description,
            reviews,
            product.Rating,
            product.NumReviews,
            product.Price,
            product.CountInStock);
    }

    public async Task<ProductIndexDto> CreateProductAsync(CreateProductDto product)
    {
        var productDocument = MapProductDtoToDocument(product);

        await _repositoryManager.Product.AddAsync(productDocument);
        var productIndex = MapProductToProductIndexDto(productDocument);

        return productIndex;
    }

    public Task<(IEnumerable<ProductIndexDto> products, string ids)> CreateProductCollectionAsync(IEnumerable<CreateProductDto> productCollection)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteProductAsync(string productId)
    {
        var objectId = new ObjectId(productId);
        await _repositoryManager.Product.DeleteAsync(u => u.Id == objectId);
    }

    public Task<IEnumerable<ProductIndexDto>> GetByIdsAsync(IEnumerable<string> ids)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateProductAsync(string productid, UpdateProductDto productForUpdate)
    {
        var productDocument = await _repositoryManager.Product.GetAsync(productid);

        productDocument.Name = productForUpdate.Name;
        productDocument.Image = productForUpdate.Image;
        productDocument.NumReviews = productForUpdate.NumReviews;
        productDocument.Price = productForUpdate.Price;
        productDocument.Brand = productForUpdate.Brand;
        productDocument.Category = productForUpdate.Category;
        productDocument.CountInStock = productForUpdate.CountInStock;
        productDocument.Description = productForUpdate.Description;
        productDocument.UserId = productForUpdate.UserId;

        //TODO: update reviews for the product

        _repositoryManager.Product.Update(productDocument);
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


    private List<ProductIndexDto> MapToProductIndexDtos(IEnumerable<Product> products)
    {
        var productsDto = new List<ProductIndexDto>();  
        foreach (var product in products)
        {
            var productDto = MapProductToProductIndexDto(product);
            productsDto.Add(productDto);
        }

        return productsDto;
    }

    private ProductIndexDto MapProductToProductIndexDto(Product product)
    {
        return new ProductIndexDto(
                product.Id.ToString(),
                product?.Name,
                product?.Image,
                product?.Brand,
                product?.Category,
                product.NumReviews,
                product.Price,
                product.Rating);
    }

    private List<Review>? MapProductReviews(List<ReviewDto>? reviews)
    {
        if (reviews is not null)
        {
            var reviewDocuments = new List<Review>();

            foreach (var review in reviews)
            {
                var reviewDocument = new Review
                {
                    Name=review.Name,
                    Comment = review.Comment,
                    Rating =review.Rating
                };

                reviewDocuments.Add(reviewDocument);
            }
        }
        return Enumerable.Empty<Review>().ToList();
    }


    private List<ReviewDto>? MapReviewsToReviewDto(List<Review>? reviews)
    {
        if (reviews is not null)
        {
            var reviewDtos = new List<ReviewDto>();

            foreach (var review in reviews)
            {
                var reviewDto = new ReviewDto(review.Name, review.Rating, review.Comment);
                reviewDtos.Add(reviewDto);
            }
        }
        return Enumerable.Empty<ReviewDto>().ToList();
    }

    #endregion
}
