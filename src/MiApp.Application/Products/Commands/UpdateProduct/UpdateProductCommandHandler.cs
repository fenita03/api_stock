using FluentValidation;
using MediatR;
using MiApp.Application.Abstractions;
using MiApp.Application.Common;

namespace MiApp.Application.Products.Commands.UpdateProduct;

public sealed class UpdateProductCommandHandler(
    IProductRepository productRepository,
    IValidator<UpdateProductCommand> validator)
    : IRequestHandler<UpdateProductCommand, ProductDto?>
{
    public async Task<ProductDto?> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product is null)
        {
            return null;
        }

        if (!await productRepository.CategoryExistsAsync(request.CategoryId, cancellationToken))
        {
            throw new KeyNotFoundException("Category not found.");
        }

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.Stock = request.Stock;
        product.CategoryId = request.CategoryId;

        productRepository.Update(product);
        await productRepository.SaveChangesAsync(cancellationToken);

        product = await productRepository.GetByIdAsync(product.Id, cancellationToken) ?? product;

        return new ProductDto(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.Stock,
            product.CategoryId,
            product.Category?.Name ?? string.Empty);
    }
}
