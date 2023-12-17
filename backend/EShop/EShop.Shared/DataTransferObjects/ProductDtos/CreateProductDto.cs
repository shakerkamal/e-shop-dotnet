namespace EShop.Shared.DataTransferObjects.ProductDtos;

public record CreateProductDto(
    string UserId, 
    string? Name,
    string? Image,
    string? Brand,
    string? Category,
    string? Description,
    List<Review>? Reviews,
    double Rating,
    int NumReviews,
    double Price,
    int CountInStock);

public record Review(string? Name, int Rating, string? Comment);
