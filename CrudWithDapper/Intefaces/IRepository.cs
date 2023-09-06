namespace CrudWithDapper.Intefaces;

public interface IRepository<TEntity, TModel>
{
    public Task<bool> CreateAsync(TModel entity);

    public Task<bool> UpdateAsync(long id, TModel entity);

    public Task<bool> DeleteAsync(long id);

    public Task<TEntity> GetByIdAsync(long id);

    public Task<IList<TEntity>> GetAllAsync();
}
