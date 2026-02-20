using MediatR;
using Microsoft.AspNetCore.Mvc;
using EShop.Application.DTOS.Product;
using EShop.Persistence.Services.Concretes;
using EShop.Application.Behaviors.Common.Query.Product.GetAll;

namespace EShop.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductWithMediatRController : ControllerBase
{
    private readonly ProductService _productService;
    private readonly IMediator _mediator;

    public ProductWithMediatRController(IMediator mediator, ProductService productService)
    {
        _mediator = mediator;
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 10)
    {
        var response = await _mediator.Send(new GetAllProductRequest
        {
            Page = page,
            PageSize = pageSize
        });

        return Ok(response);
    }


    [HttpPost("AddProduct")]
    public async Task<IActionResult> AddProduct([FromBody] AddProductDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(model);

        var result = await _productService.AddAsync(model);

        if (!result)
            return BadRequest();

        return StatusCode(204);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] int id)
     => await _productService.DeleteAsync(id) ? StatusCode(204) : BadRequest();


    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _productService.GetByIdAsync(id);

        if (result is null)
            return BadRequest();

        return Ok(result);
    }

    [HttpPut("Update/{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductDTO model)
    {
        var result = await _productService.UpdateAsync(id, model);

        if (!result)
            return BadRequest();

        return StatusCode(204);
    }

    [HttpGet("GetProductsByCategoryId/{categoryId}")]
    public async Task<IActionResult> GetProductsByCategoryId([FromRoute] int categoryId)
    {
        var result = await _productService.GetProductsByCategoryId(categoryId);

        if (result is null)
            return BadRequest();

        return Ok(result);
    }

}
