using MediatR;
using MiApp.Application.Abstractions;

namespace MiApp.Application.Products.Commands.DeleteProduct;

public sealed class DeleteProductCommandHandler(IProductRepository productRepository) : IRequestHandler<DeleteProductCommand, bool>
{
    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product is null)
        {
            return false;
        }

        productRepository.Delete(product);
        await productRepository.SaveChangesAsync(cancellationToken);
        return true;
    }
}
