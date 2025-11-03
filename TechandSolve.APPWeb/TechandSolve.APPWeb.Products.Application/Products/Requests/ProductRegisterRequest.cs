namespace TechAndSolve.APPWeb.Products.Application.Products.Requests;

public record ProductRegisterRequest(
    string Name,
    string Description,
    decimal Price,
    int Stock);