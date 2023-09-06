using CrudWithEntity.Contexts;
using CrudWithEntity.Interfaces.Products;
using CrudWithEntity.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CrudWithEntity.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _dbContext;

    public ProductRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product> AddAsync(Product entity)
    {
        await _dbContext.Products.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<long> CountAsync()
    {
        return await _dbContext.Products.CountAsync();
    }

    public async Task<bool> DeleteAsync(Expression<Func<Product, bool>> expression)
    {
        var productsToDelete = _dbContext.Products.Where(expression);
        _dbContext.Products.RemoveRange(productsToDelete);
        await _dbContext.SaveChangesAsync();
        return true; // You can handle errors and return false if necessary
    }

    public IQueryable<Product> SelectAll(Expression<Func<Product, bool>> expression = null)
    {
        return expression == null
            ? _dbContext.Products
            : _dbContext.Products.Where(expression);
    }

    public async Task<Product> SelectAsync(Expression<Func<Product, bool>> expression)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(expression);
    }

    public async Task<Product> UpdateAsync(Product entity)
    {
        _dbContext.Products.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
