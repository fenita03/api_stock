using Microsoft.EntityFrameworkCore;
using MiApp.Application.Abstractions;
using MiApp.Domain.Entities;
using MiApp.Infrastructure.Data;

namespace MiApp.Infrastructure.Repositories;

public sealed class CategoryRepository(AppDbContext dbContext) : ICategoryRepository
{
    public async Task<IReadOnlyList<Category>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Categories
            .AsNoTracking()
            .OrderBy(category => category.Id)
            .ToListAsync(cancellationToken);
    }

    public Task<Category?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return dbContext.Categories.FirstOrDefaultAsync(category => category.Id == id, cancellationToken);
    }

    public Task AddAsync(Category category, CancellationToken cancellationToken)
    {
        return dbContext.Categories.AddAsync(category, cancellationToken).AsTask();
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}
