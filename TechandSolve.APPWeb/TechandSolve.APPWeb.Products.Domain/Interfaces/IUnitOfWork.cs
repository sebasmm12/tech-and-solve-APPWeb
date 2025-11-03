namespace TechAndSolve.APPWeb.Products.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task SaveChangesAsync();
}