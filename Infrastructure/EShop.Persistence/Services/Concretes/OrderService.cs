using EShop.Application.DTOS;
using EShop.Application.DTOS.Order;
using EShop.Application.Repositories;
using EShop.Domain.Entities.Concretes;
using EShop.Application.Services.Abstracts;
using EShop.Application.DTOS.Product;
using EShop.Persistence.Repositories;

namespace EShop.Persistence.Services.Concretes;

public class OrderService : IOrderService
{

    private readonly IOrderReadRepository _orderReadRepository;
    private readonly IOrderWriteRepository _orderWriteRepository;
    
    public OrderService(IOrderReadRepository orderReadRepository, IOrderWriteRepository orderWriteRepository)
    {
        _orderReadRepository = orderReadRepository;
        _orderWriteRepository = orderWriteRepository;
    }
    
    public async Task<bool> AddAsync(AddOrderDto model)
    {
        var newOrder = new Order()
        {
            OrderNumber = model.OrderNumber,
            OrderDate = model.OrderDate,
            OrderNote = model.OrderNote,
            Total = model.Total,
            CustomerId = model.CostumerId
        };
    
        await _orderWriteRepository.AddAsync(newOrder);
        await _orderWriteRepository.SaveChangeAsync();
    
        return true;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _orderReadRepository.GetByIdAsync(id);
    
        if (product is null)
            return false;
    
        await _orderWriteRepository.Delete(id);
        await _orderWriteRepository.SaveChangeAsync();
    
        return true;
    }
    
    public async Task<IEnumerable<AllOrderDto>> GetAllAsync(PaginationDTO model)
    {
        var orders = await _orderReadRepository.GetAllAsync();
    
        var orderWithPagionation = orders
                                     .Skip(model.Page * model.PageSize)
                                     .Take(model.PageSize)
                                     .ToList();
    
        var allOrderDto = orderWithPagionation
            .Select(x => new AllOrderDto()
            {
                OrderNumber = x.OrderNumber,
                OrderDate = x.OrderDate,
                OrderNote = x.OrderNote,
                Total = x.Total,
                CostumerName = x.Customer.Name
            });
    
        return allOrderDto;
    }

    public async Task<AllOrderDto> GetByIdAsync(int id)
    {
        var order = await _orderReadRepository.GetByIdAsync(id);
    
        if (order is null)
            return null;
    
        var mappedData = new AllOrderDto()
        {
            OrderNumber = order.OrderNumber,
            OrderDate = order.OrderDate,
            OrderNote = order.OrderNote,
            Total = order.Total,
            CostumerName = order.Customer.Name
        };
    
        return mappedData;
    }
    
    public async Task<List<AllOrderDto>> GetOrdersByCostumerId(int costumerId)
    {
        return await _orderReadRepository.GetOrdersByCostumerId(costumerId);
    }
    
    public async Task<bool> UpdateAsync(int id, UpdateOrderDto model)
    {
        var order = await _orderReadRepository.GetByIdAsync(id);
    
        if (order is null)
            return false;

        order.OrderNumber = model.OrderNumber;
        order.OrderDate = model.OrderDate;
        order.OrderNote = model.OrderNote;
        order.Total = model.Total;
        order.CustomerId = model.CostumerId;


        await _orderWriteRepository.SaveChangeAsync();
    
        return true;
    }
}
