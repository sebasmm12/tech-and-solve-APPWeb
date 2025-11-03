using TechAndSolve.APPWeb.Products.Application.Products.Requests;
using TechAndSolve.APPWeb.Products.Application.Products.Responses;

namespace TechAndSolve.APPWeb.Products.Application.Products.Services;

public interface IProductsService
{
    Task<IEnumerable<ProductResponse>> GetAllAsync();

    Task<ProductResponse> GetByIdAsync(int productId);

    Task<int> CreateAsync(ProductRegisterRequest productRegisterRequest);

    Task UpdateAsync(ProductUpdateRequest productUpdateRequest);

    Task DeleteAsync(int productId);
}