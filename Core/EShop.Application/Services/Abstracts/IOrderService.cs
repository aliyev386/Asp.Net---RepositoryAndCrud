using EShop.Application.DTOS.Product;
using EShop.Application.DTOS;
using EShop.Application.DTOS.Order;

namespace EShop.Application.Services.Abstracts;

public interface IOrderService
{
    Task<IEnumerable<AllOrderDto>> GetAllAsync(PaginationDTO model);
    Task<bool> AddAsync(AddOrderDto model);
    Task<bool> DeleteAsync(int id);
    Task<AllOrderDto> GetByIdAsync(int id);
    Task<bool> UpdateAsync(int id, UpdateOrderDto model);
    Task<List<AllOrderDto>> GetOrdersByCostumerId(int categoryId);

}
