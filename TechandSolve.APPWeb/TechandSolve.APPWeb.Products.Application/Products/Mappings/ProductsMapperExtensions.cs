using TechAndSolve.APPWeb.Products.Application.Products.Requests;
using TechAndSolve.APPWeb.Products.Application.Products.Responses;
using TechAndSolve.APPWeb.Products.Domain.Products;

namespace TechAndSolve.APPWeb.Products.Application.Products.Mappings;

public static class ProductsMapperExtensions
{
    public static Product ToProduct(this ProductRegisterRequest productRegisterRequest)
    {
        var product = new Product
        {
            Name = productRegisterRequest.Name,
            Description = productRegisterRequest.Description,
            Price = productRegisterRequest.Price,
            Stock = productRegisterRequest.Stock
        };

        return product;
    }

    public static Product ToProduct(this ProductUpdateRequest productUpdateRequest, Product product)
    {
        product.Name = productUpdateRequest.Name;
        product.Description = productUpdateRequest.Description;
        product.Price = productUpdateRequest.Price;
        product.Stock = productUpdateRequest.Stock;
            
        return product;
    }

    public static ProductResponse ToProductResponse(this Product product)
    {
        var productResponse = new ProductResponse(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.Stock);

        return productResponse;
    }
}