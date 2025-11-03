using TechAndSolve.APPWeb.Products.Domain.Bases;

namespace TechAndSolve.APPWeb.Products.Domain.Interfaces;

public interface IRepository<TEntity> where TEntity: IEntityBase
{
    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<TEntity?> GetByIdAsync(int id);

    Task<bool> ExistsAsync(int id);

    void Add(TEntity entity);

    void Update(TEntity entity);

    void Delete(TEntity entity);
}