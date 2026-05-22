using MediatR;
using MiApp.Application.Common;

namespace MiApp.Application.Products.Queries.GetProducts;

public sealed record GetProductsQuery : IRequest<IReadOnlyList<ProductDto>>;
