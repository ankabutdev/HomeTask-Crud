using CrudWithEntity.Contexts;
using CrudWithEntity.Interfaces.Products;
using CrudWithEntity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudWithEntity.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repository;
    private readonly AppDbContext _dbContext;

    public ProductsController(IProductRepository repository,
        AppDbContext dbContext)
    {
        _repository = repository;
        this._dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var products = _repository.SelectAll();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var product = await _repository.SelectAsync(p => p.Id == id);

        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Product product)
    {
        if (product == null)
            return BadRequest();

        await _repository.AddAsync(product);

        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Product product)
    {
        if (product == null || id != product.Id)
            return BadRequest();

        var existingProduct = await _repository.SelectAsync(p => p.Id == id);

        if (existingProduct == null)
            return NotFound();

        _dbContext.Entry(existingProduct).State = EntityState.Detached;

        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;

        _dbContext.Entry(existingProduct).State = EntityState.Modified;

        await _repository.SaveAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existingProduct = await _repository.SelectAsync(p => p.Id == id);

        if (existingProduct == null)
            return NotFound();

        await _repository.DeleteAsync(p => p.Id == id);

        return Ok();
    }
}
