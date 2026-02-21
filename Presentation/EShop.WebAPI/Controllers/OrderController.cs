using EShop.Application.DTOS;
using EShop.Application.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using EShop.Application.DTOS.Order;

namespace EShop.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : Controller
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] PaginationDTO model)
     => Ok(await _orderService.GetAllAsync(model));


    [HttpPost("AddOrder")]
    public async Task<IActionResult> AddOrder([FromBody] AddOrderDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(model);

        var result = await _orderService.AddAsync(model);

        if (!result)
            return BadRequest();

        return StatusCode(204);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteOrder([FromRoute] int id)
     => await _orderService.DeleteAsync(id) ? StatusCode(204) : BadRequest();


    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _orderService.GetByIdAsync(id);

        if (result is null)
            return BadRequest();

        return Ok(result);
    }

    [HttpPut("Update/{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateOrderDto model)
    {
        var result = await _orderService.UpdateAsync(id, model);

        if (!result)
            return BadRequest();

        return StatusCode(204);
    }

    [HttpGet("GetOrdersByCostumerId/{costumerId}")]
    public async Task<IActionResult> GetOrdersByCostumerId([FromRoute] int categoryId)
    {
        var result = await _orderService.GetOrdersByCostumerId(categoryId);

        if (result is null)
            return BadRequest();

        return Ok(result);
    }
    //Product
}
