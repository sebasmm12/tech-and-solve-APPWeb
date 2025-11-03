using Microsoft.EntityFrameworkCore;
using TechAndSolve.APPWeb.Products.Domain.Products;

namespace TechAndSolve.APPWeb.Products.Infrastructure.Persistence.Repositories.Products;

public class ProductsRepository(
    ApplicationDbContext applicationDbContext) : Repository<Product>(applicationDbContext), IProductsRepository
{
    public async Task<Product?> GetByNameAsync(string name)
    {
        var product = await dbSet
            .FirstOrDefaultAsync(p => p.Name == name);

        return product;
    }
}