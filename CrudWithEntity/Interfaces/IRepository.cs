using System.Linq.Expressions;

namespace CrudWithEntity.Interfaces;

public interface IRepository<T>
{
    public Task<T> AddAsync(T entity);

    public Task<T> UpdateAsync(T entity);

    public Task<bool> DeleteAsync(Expression<Func<T, bool>> expression);

    public Task<T> SelectAsync(Expression<Func<T, bool>> expression);

    public IQueryable<T> SelectAll(Expression<Func<T, bool>> expression = null!);

    public Task<long> CountAsync();

    public Task SaveAsync();
}