using FluentValidation;
using TechAndSolve.APPWeb.Products.Application.Products.Mappings;
using TechAndSolve.APPWeb.Products.Application.Products.Requests;
using TechAndSolve.APPWeb.Products.Application.Products.Responses;
using TechAndSolve.APPWeb.Products.Domain.Interfaces;
using TechAndSolve.APPWeb.Products.Domain.Products;

namespace TechAndSolve.APPWeb.Products.Application.Products.Services;

public class ProductsService
    (IProductsRepository productsRepository,
     IUnitOfWork unitOfWork,
     IValidator<ProductRegisterRequest> productRegisterRequestValidator,
     IValidator<ProductUpdateRequest> productUpdateRequestValidator): IProductsService
{
    public async Task<IEnumerable<ProductResponse>> GetAllAsync()
    {
        var products = await productsRepository.GetAllAsync();

        var productsResponse = products.Select(p => p.ToProductResponse());

        return productsResponse;
    }

    public async Task<ProductResponse> GetByIdAsync(int productId)
    {
        var product = await productsRepository.GetByIdAsync(productId);

        if (product is null)
            throw new KeyNotFoundException("Producto no encontrado.");

        var productResponse = product.ToProductResponse();

        return productResponse;
    }

    public async Task<int> CreateAsync(ProductRegisterRequest productRegisterRequest)
    {
        await productRegisterRequestValidator.ValidateAndThrowAsync(productRegisterRequest);

        var product = productRegisterRequest.ToProduct();

        productsRepository.Add(product);

        await unitOfWork.SaveChangesAsync();

        return product.Id;
    }

    public async Task UpdateAsync(ProductUpdateRequest productUpdateRequest)
    {
        await productUpdateRequestValidator.ValidateAndThrowAsync(productUpdateRequest);

        var product = await productsRepository.GetByIdAsync(productUpdateRequest.Id);

        productUpdateRequest.ToProduct(product!);

        productsRepository.Update(product!);

        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int productId)
    {
        var product = await productsRepository.GetByIdAsync(productId);

        if (product is null)
            throw new KeyNotFoundException("Producto no encontrado.");

        productsRepository.Delete(product);

        await unitOfWork.SaveChangesAsync();
    }
}