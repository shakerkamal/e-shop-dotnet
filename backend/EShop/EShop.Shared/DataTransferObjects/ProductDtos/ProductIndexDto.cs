namespace EShop.Shared.DataTransferObjects.ProductDtos;

public record ProductIndexDto(
    string Id,
    string Name, 
    string Image,
    string Brand,
    string Category,
    int NumReviews,
    double Price,
    double Rating);
