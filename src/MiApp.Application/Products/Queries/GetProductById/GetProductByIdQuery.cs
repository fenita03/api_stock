using MediatR;
using MiApp.Application.Common;

namespace MiApp.Application.Products.Queries.GetProductById;

public sealed record GetProductByIdQuery(int Id) : IRequest<ProductDto?>;
