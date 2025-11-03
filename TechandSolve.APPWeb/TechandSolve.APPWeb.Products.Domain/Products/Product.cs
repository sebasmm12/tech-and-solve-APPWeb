using TechAndSolve.APPWeb.Products.Domain.Bases;

namespace TechAndSolve.APPWeb.Products.Domain.Products;

public class Product : EntityBase
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int Stock { get; set; }
}