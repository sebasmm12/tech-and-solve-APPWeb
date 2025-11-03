using TechAndSolve.APPWeb.Products.Domain.Interfaces;

namespace TechAndSolve.APPWeb.Products.Domain.Products;

public interface IProductsRepository : IRepository<Product>
{
    Task<Product?> GetByNameAsync(string name);
}