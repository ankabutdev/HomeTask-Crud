using CrudWithDapper.Entites;

namespace CrudWithDapper.Intefaces;

public interface IProductRepository : IRepository<Product, ProductDto>
{
}
