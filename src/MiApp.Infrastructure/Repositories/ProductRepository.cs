using Microsoft.EntityFrameworkCore;
using MiApp.Application.Abstractions;
using MiApp.Domain.Entities;
using MiApp.Infrastructure.Data;

namespace MiApp.Infrastructure.Repositories;

public sealed class ProductRepository(AppDbContext dbContext) : IProductRepository
{
    public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Products
            .Include(product => product.Category)
            .AsNoTracking()
            .OrderBy(product => product.Id)
            .ToListAsync(cancellationToken);
    }

    public Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return dbContext.Products
            .Include(product => product.Category)
            .FirstOrDefaultAsync(product => product.Id == id, cancellationToken);
    }

    public Task AddAsync(Product product, CancellationToken cancellationToken)
    {
        return dbContext.Products.AddAsync(product, cancellationToken).AsTask();
    }

    public void Update(Product product)
    {
        dbContext.Products.Update(product);
    }

    public void Delete(Product product)
    {
        dbContext.Products.Remove(product);
    }

    public Task<bool> CategoryExistsAsync(int categoryId, CancellationToken cancellationToken)
    {
        return dbContext.Categories.AnyAsync(category => category.Id == categoryId, cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}
