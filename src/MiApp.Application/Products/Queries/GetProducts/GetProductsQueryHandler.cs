using MediatR;
using MiApp.Application.Abstractions;
using MiApp.Application.Common;

namespace MiApp.Application.Products.Queries.GetProducts;

public sealed class GetProductsQueryHandler(IProductRepository productRepository)
    : IRequestHandler<GetProductsQuery, IReadOnlyList<ProductDto>>
{
    public async Task<IReadOnlyList<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAllAsync(cancellationToken);

        return products
            .Select(product => new ProductDto(
                product.Id,
                product.Name,
                product.Description,
                product.Price,
                product.Stock,
                product.CategoryId,
                product.Category?.Name ?? string.Empty))
            .ToList();
    }
}
