using System.Linq.Expressions;

namespace CarAuction.Domain.Interfaces
{
  /// <summary>
  ///   Generic repository interface for basic CRUD operations on entities of type <typeparamref name="T"/>.
  /// </summary>
  /// <typeparam name="T">The type of entity managed by the repository.</typeparam>
  public interface IRepository<T>
  {
    /// <summary>
    ///   Retrieves entities based on optional filtering and ordering criteria.
    /// </summary>
    /// <param name="predicate">Optional. A predicate to filter entities.</param>
    /// <param name="orderBy">Optional. A function to specify ordering of entities.</param>
    /// <returns>The result is a queryable collection of entities.</returns>
    Task<IQueryable<T>> GetAll(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

    /// <summary>
    ///   Retrieves an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <returns>The result is the entity.</returns>
    Task<T> Get(object id);

    /// <summary>
    ///   Adds a new entity to the repository.
    /// </summary>
    /// <param name="entity">The entity to be added.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Add(T entity);

    /// <summary>
    ///   Updates an existing entity in the repository.
    /// </summary>
    /// <param name="entity">The entity to be updated.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Update(T entity);

    /// <summary>
    ///   Deletes an entity from the repository.
    /// </summary>
    /// <param name="entity">The entity to be deleted.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Delete(T entity);
  }
}
