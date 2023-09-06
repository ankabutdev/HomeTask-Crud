using CrudWithDapper.Entites;
using CrudWithDapper.Intefaces;
using Dapper;

namespace CrudWithDapper.Repositories;

public class ProductRepository : BaseRepository, IProductRepository
{
    public async Task<bool> CreateAsync(ProductDto entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO public.\"Products\"(\"Name\", \"Price\")" +
                "VALUES (@Name, @Price);";
            var result = await _connection.ExecuteAsync(query, entity);
            return result > 0;
        }
        catch
        {
            return false;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<bool> DeleteAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"DELETE FROM public.\"Products\" WHERE \"Id\"=@Id";
            var result = await _connection.ExecuteAsync(query, new { Id = id });
            return result > 0;
        }
        catch
        {
            return false;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<IList<Product>> GetAllAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM \"Products\" order by \"Id\" desc";
            var result = (await _connection.QueryAsync<Product>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<Product>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<Product> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM \"Products\" WHERE \"Id\"=@Id";
            var result = await _connection.QueryFirstOrDefaultAsync<Product>(query, new { Id = id });
            return result;
        }
        catch
        {
            return new Product();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<bool> UpdateAsync(long id, ProductDto entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "UPDATE public.\"Products\" SET" +
                " \"Name\"=@Name, \"Price\"=@Price " +
                $"WHERE \"Id\"={id};";
            var result = await _connection.ExecuteAsync(query, entity);
            return result > 0;
        }
        catch
        {
            return false;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }
}
