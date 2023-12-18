namespace EShop.Shared.DataTransferObjects.ProductDtos;

public record ProductDetailsDto(
    string Id,
    string UserId,
    string? Name,
    string? Image,
    string? Brand,
    string? Category,
    string? Description,
    List<ReviewDto>? Reviews,
    double Rating,
    int NumReviews,
    double Price,
    int CountInStock);
