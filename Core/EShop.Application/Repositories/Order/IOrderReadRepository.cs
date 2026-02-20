using EShop.Domain.Entities.Concretes;
using EShop.Application.Repositories.Common;
using EShop.Application.DTOS.Product;
using EShop.Application.DTOS.Order;

namespace EShop.Application.Repositories;

public interface IOrderReadRepository : IReadGenericRepository<Order>
{
    Task<List<AllOrderDto>> GetOrdersByCostumerId(int costumerId);
}
