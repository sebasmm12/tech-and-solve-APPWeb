namespace TechAndSolve.APPWeb.Products.Application.Products.Requests;

public record ProductUpdateRequest(
    int Id,
    string Name,
    string Description,
    decimal Price,
    int Stock);