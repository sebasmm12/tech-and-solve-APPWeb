using Microsoft.EntityFrameworkCore;
using TechAndSolve.APPWeb.Products.Domain.Bases;
using TechAndSolve.APPWeb.Products.Domain.Interfaces;

namespace TechAndSolve.APPWeb.Products.Infrastructure.Persistence.Repositories;

public class Repository<TEntity>
    (ApplicationDbContext applicationDbContext): IRepository<TEntity>
    where TEntity : class, IEntityBase
{
    protected readonly DbSet<TEntity> dbSet = applicationDbContext.Set<TEntity>();

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var entities = await dbSet
            .AsNoTracking()
            .Where(entity => !entity.IsDeleted)
            .ToListAsync();

        return entities;
    }

    public async Task<TEntity?> GetByIdAsync(int key)
    {
        var entity = await dbSet
            .FirstOrDefaultAsync(entity => !entity.IsDeleted &&
                                      entity.Id == key);

        return entity;
    }

    public Task<bool> ExistsAsync(int id)
    {
        var exists = dbSet
            .AsNoTracking()
            .AnyAsync(e => !e.IsDeleted &&
                           e.Id == id);

        return exists;
    }

    public void Add(TEntity entity)
    {
        entity.CreatedAt = DateTime.Now;

        dbSet.Add(entity);
    }

    public void Update(TEntity entity)
    {
        entity.UpdatedAt = DateTime.Now;
        
        dbSet.Update(entity);
    }

    public void Delete(TEntity entity)
    {
        entity.UpdatedAt = DateTime.Now;
        entity.IsDeleted = true;

        dbSet.Update(entity);
    }
}