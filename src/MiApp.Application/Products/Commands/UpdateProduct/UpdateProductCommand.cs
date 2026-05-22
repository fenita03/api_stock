using MediatR;
using MiApp.Application.Common;

namespace MiApp.Application.Products.Commands.UpdateProduct;

public sealed record UpdateProductCommand(
    int Id,
    string Name,
    string? Description,
    decimal Price,
    int Stock,
    int CategoryId) : IRequest<ProductDto?>;
