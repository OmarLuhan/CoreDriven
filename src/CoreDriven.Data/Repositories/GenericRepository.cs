using Microsoft.EntityFrameworkCore;

namespace CoreDriven.Data.Repositories;
public interface IGenericRepository<T> where T : class
{
    IQueryable<T> Query(bool track= false);
    Task <T> GetByIdAsync(int id);
    Task <T>AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}
public class GenericRepository<T>(Entities.AppContext context)  :IGenericRepository<T>
    where T : class
{
    private readonly DbSet<T> _dbSet= context.Set<T>();
    public IQueryable<T> Query(bool track = false)
    {
        var query = track ? _dbSet : _dbSet.AsNoTracking();
        return query;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        T? entity = await _dbSet.FindAsync(id);
        if (entity == null)
        {
            throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with ID {id} not found.");
        }
        return entity;
    }

    public async Task<T> AddAsync(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
        }

        await _dbSet.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
        }

        _dbSet.Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        T? entity = await _dbSet.FindAsync(id);
        if (entity == null)
        {
            throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with ID {id} not found.");
        }

        _dbSet.Remove(entity);
        await context.SaveChangesAsync();
    }
}