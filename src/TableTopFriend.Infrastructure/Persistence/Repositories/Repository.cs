using System.Linq.Expressions;
using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Domain.Common.Models;
using TableTopFriend.Domain.Common.ValueObjects;
using TableTopFriend.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TableTopFriend.Infrastructure.Persistence;

public abstract class Repository<T, TId, TIdType>
    where T : AggregateRoot<TId, TIdType>
    where TId : AggregateRootId<TIdType>
{
    protected readonly IApplicationDbContext DbContext;
    protected readonly DbSet<T> EntityDbSet;
    protected readonly IUnitOfWork UnitOfWork;
    protected Repository(IApplicationDbContext dbContext, IUnitOfWork unitOfWork)
    {
        DbContext = dbContext;
        EntityDbSet = DbContext.Set<T>();
        UnitOfWork = unitOfWork;
    }

    /// <summary>
    /// Search for a collection of items of Type TEntity based on a lambda expression to filter.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A Task of type IEnumerable of TEntity or Null if the TEntities was not found</returns>
    public virtual async Task<IEnumerable<T>> SearchAsNoTracking(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await EntityDbSet
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync(cancellationToken);
    }

    public virtual async Task<IEnumerable<T>> Search(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await EntityDbSet
            .Where(predicate)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Gets a TEntity by the Id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A Task of type TEntity or Null if the TEntity was not found</returns>
    public virtual async Task<T?> GetById(
        AggregateRootId<TIdType> id,
        CancellationToken cancellationToken)
    {
        return await EntityDbSet.FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }

    /// <summary>
    /// Gets all TEntity in the specific table.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>A Task of type IEnumerable of TEntity or Null if the TEntities was not found</returns>
    public virtual async Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken)
    {
        return await EntityDbSet.ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Add a TEntity to the database.
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The entity updated with the Id and CreatedAt properties.</returns>
    public virtual async Task<T> Add(
        T entity,
        CancellationToken cancellationToken)
    {
        await EntityDbSet.AddAsync(entity, cancellationToken);
        return entity;
    }

    /// <summary>
    /// Updates a TEntity to the database.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>The entity updated with the Id and UpdatedAt properties.</returns>
    public virtual async Task<T> Update(T entity)
    {
        await Task.FromResult(EntityDbSet.Update(entity));
        return entity;
    }

    /// <summary>
    /// Removes a TEntity from the database.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>The entity that was removed.</returns>
    public virtual async Task<T> Remove(T entity)
    {
        await Task.FromResult(EntityDbSet.Remove(entity));
        return entity;
    }
}
