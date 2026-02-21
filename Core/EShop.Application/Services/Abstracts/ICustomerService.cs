using EShop.Application.DTOS;
using EShop.Application.DTOS.Customer;
using EShop.Application.DTOS.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Services.Abstracts;

public interface ICustomerService
{
    Task<IEnumerable<AllCustomerDto>> GetAllAsync(PaginationDTO model);
    Task<bool> AddAsync(AddCustomerDto model);
    Task<bool> DeleteAsync(int id);
    Task<AllCustomerDto> GetByIdAsync(int id);
    Task<bool> UpdateAsync(int id, UpdateCustomerDto model);
}
