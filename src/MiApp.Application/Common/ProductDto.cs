namespace MiApp.Application.Common;

public sealed record ProductDto(
    int Id,
    string Name,
    string? Description,
    decimal Price,
    int Stock,
    int CategoryId,
    string CategoryName);
