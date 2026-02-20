using EShop.Persistence.Datas;
using EShop.Application.Repositories;
using EShop.Domain.Entities.Concretes;
using EShop.Persistence.Repositories.Common;
using EShop.Application.DTOS.Product;
using EShop.Application.DTOS.Order;

namespace EShop.Persistence.Repositories;

public class OrderReadRepository : ReadGenericRepository<Order>, IOrderReadRepository
{
    public OrderReadRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
    //public async Task<List<AllProductDTO>>
    public async Task<List<AllOrderDto>> GetOrdersByCostumerId(int costumerId)
    {
        var orders = _context
                        .Orders
                        .Where(x => x.CustomerId == costumerId)
                        .Select(x => new AllOrderDto()
                        {
                            OrderNumber = x.OrderNumber,
                            OrderDate = x.OrderDate,
                            OrderNote = x.OrderNote,
                            Total = x.Total,
                            CostumerName = x.Customer.Name,
                        }).ToList();


        return orders;
    }

}
