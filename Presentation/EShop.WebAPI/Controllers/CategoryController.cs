using EShop.Application.DTOS;
using Microsoft.AspNetCore.Mvc;
using EShop.Application.Repositories;
using EShop.Application.DTOS.Category;
using EShop.Domain.Entities.Concretes;
using EShop.Persistence.Services.Concretes;

namespace EShop.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly CategoryService _categoryService;
    private readonly ICategoryWriteRepository _categoryWriteRepository;

    public CategoryController(ICategoryWriteRepository categoryWriteRepository, CategoryService categoryService)
    {
        _categoryWriteRepository = categoryWriteRepository;
        _categoryService = categoryService;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] PaginationDTO model)
     => Ok(await _categoryService.GetAllAsync(model));


    [HttpPost("AddCategory")]
    public async Task<IActionResult> Add([FromBody] AddCategoryDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(model);

        var newCategory = new Category()
        {
            Name = model.Name,
            Description = model.Description
        };

        await _categoryWriteRepository.AddAsync(newCategory);
        await _categoryWriteRepository.SaveChangeAsync();

        return Ok(newCategory);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] int id)
     => await _categoryService.DeleteAsync(id) ? StatusCode(204) : BadRequest();


    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _categoryService.GetByIdAsync(id);

        if (result is null)
            return BadRequest();

        return Ok(result);
    }

    [HttpPut("Update/{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCagetoryDto model)
    {
        var result = await _categoryService.UpdateAsync(id, model);

        if (!result)
            return BadRequest();

        return StatusCode(204);
    }

}
