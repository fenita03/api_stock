using MiApp.Domain.Entities;

namespace MiApp.Application.Abstractions;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken);
    Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task AddAsync(Product product, CancellationToken cancellationToken);
    void Update(Product product);
    void Delete(Product product);
    Task<bool> CategoryExistsAsync(int categoryId, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
