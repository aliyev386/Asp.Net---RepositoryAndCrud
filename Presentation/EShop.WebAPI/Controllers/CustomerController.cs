using EShop.Application.DTOS;
using EShop.Application.DTOS.Customer;
using EShop.Application.DTOS.Product;
using EShop.Application.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace EShop.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CustomerController : Controller
{
    private readonly ICustomerService _customerService;
    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] PaginationDTO model)
     => Ok(await _customerService.GetAllAsync(model));


    [HttpPost("AddCustomer")]
    public async Task<IActionResult> AddProduct([FromBody] AddCustomerDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(model);

        var result = await _customerService.AddAsync(model);

        if (!result)
            return BadRequest();

        return StatusCode(204);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] int id)
     => await _customerService.DeleteAsync(id) ? StatusCode(204) : BadRequest();


    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _customerService.GetByIdAsync(id);

        if (result is null)
            return BadRequest();

        return Ok(result);
    }

    [HttpPut("Update/{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCustomerDto model)
    {
        var result = await _customerService.UpdateAsync(id, model);

        if (!result)
            return BadRequest();

        return StatusCode(204);
    }
}
