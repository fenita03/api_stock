using MediatR;
using MiApp.Application.Common;

namespace MiApp.Application.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(
    string Name,
    string? Description,
    decimal Price,
    int Stock,
    int CategoryId) : IRequest<ProductDto>;
