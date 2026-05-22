using MiApp.Domain.Entities;

namespace MiApp.Infrastructure.Data;

public static class DatabaseSeeder
{
    public static void Seed(AppDbContext dbContext)
    {
        if (dbContext.Categories.Any())
        {
            return;
        }

        var electronics = new Category { Name = "Electronics" };
        var food = new Category { Name = "Food" };

        dbContext.Categories.AddRange(electronics, food);
        dbContext.Products.AddRange(
            new Product
            {
                Name = "Keyboard",
                Description = "Mechanical keyboard",
                Price = 89.99m,
                Stock = 25,
                Category = electronics
            },
            new Product
            {
                Name = "Coffee",
                Description = "Ground coffee 500g",
                Price = 12.50m,
                Stock = 60,
                Category = food
            });

        dbContext.SaveChanges();
    }
}
