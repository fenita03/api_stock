using FluentValidation;
using MediatR;
using MiApp.Application.Abstractions;
using MiApp.Application.Common;
using MiApp.Domain.Entities;

namespace MiApp.Application.Products.Commands.CreateProduct;

public sealed class CreateProductCommandHandler(
    IProductRepository productRepository,
    IValidator<CreateProductCommand> validator)
    : IRequestHandler<CreateProductCommand, ProductDto>
{
    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        if (!await productRepository.CategoryExistsAsync(request.CategoryId, cancellationToken))
        {
            throw new KeyNotFoundException("Category not found.");
        }

        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Stock = request.Stock,
            CategoryId = request.CategoryId
        };

        await productRepository.AddAsync(product, cancellationToken);
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
