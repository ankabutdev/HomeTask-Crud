using CrudWithDapper.Entites;
using CrudWithDapper.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace CrudWithDapper.Controllers;

[Route("api/products")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _repository;

    public ProductController(IProductRepository repository)
    {
        this._repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
        => Ok(await _repository.GetAllAsync());

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetByIdAsync(long productId)
        => Ok(await _repository.GetByIdAsync(productId));

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] ProductDto dto)
        => Ok(await _repository.CreateAsync(dto));

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(long productId, [FromForm] ProductDto dto)
        => Ok(await _repository.UpdateAsync(productId, dto));

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(long productId)
        => Ok(await _repository.DeleteAsync(productId));

}
