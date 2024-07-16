using CarAuction.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarAuction.Infrastructure.Repositories
{
  internal class Repository<T> : IRepository<T> where T : class
  {
    private readonly DbSet<T> _dbSet;

    public Repository(DbContext dbContext)
    {
      _dbSet = dbContext.Set<T>();
    }

    public Task<IQueryable<T>> GetAll(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
    {
      IQueryable<T> query = _dbSet;

      if (predicate != null)
      {
        query = query.Where(predicate);
      }

      if (orderBy != null)
      {
        query = orderBy(query);
      }

      return Task.FromResult(query.AsNoTracking());
    }

    public async Task<T?> Get(object id) => await _dbSet.FindAsync(id);

    public async Task Add(T entity) => await _dbSet.AddAsync(entity);

    public Task Update(T entity) => Task.FromResult(_dbSet.Update(entity));

    public Task Delete(T entity) => Task.FromResult(_dbSet.Remove(entity));
  }
}
