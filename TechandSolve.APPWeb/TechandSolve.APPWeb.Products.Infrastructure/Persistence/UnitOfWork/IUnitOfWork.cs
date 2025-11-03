namespace TechAndSolve.APPWeb.Products.Infrastructure.Persistence.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    Task SaveChangesAsync();
}