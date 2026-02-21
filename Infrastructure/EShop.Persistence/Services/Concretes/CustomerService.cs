using EShop.Application.DTOS;
using EShop.Application.DTOS.Customer;
using EShop.Application.DTOS.Product;
using EShop.Application.Repositories;
using EShop.Application.Services.Abstracts;
using EShop.Domain.Entities.Concretes;
using EShop.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Persistence.Services.Concretes;

public class CustomerService : ICustomerService
{

    private readonly CustomerReadRepository _customerReadRepository;
    private readonly CustomerWriteRepository _customerWriteRepository;

    public CustomerService(CustomerReadRepository customerReadRepository, CustomerWriteRepository customerWriteRepository)
    {
        _customerReadRepository = customerReadRepository;
        _customerWriteRepository = customerWriteRepository;
    }

    public async Task<bool> AddAsync(AddCustomerDto model)
    {
        var newCustomer = new Customer()
        {
            Name = model.Name,
            Surname = model.Surname,
            ImageUrl = model.ImageUrl,
            Email = model.Email,
            Password = model.Password,
        };

        await _customerWriteRepository.AddAsync(newCustomer);
        await _customerWriteRepository.SaveChangeAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var customer = await _customerReadRepository.GetByIdAsync(id);

        if (customer is null)
            return false;

        await _customerWriteRepository.Delete(id);
        await _customerWriteRepository.SaveChangeAsync();

        return true;
    }

    public async Task<IEnumerable<AllCustomerDto>> GetAllAsync(PaginationDTO model)
    {
        var customer = await _customerReadRepository.GetAllAsync();

        var customerWithPagionation = customer
                                     .Skip(model.Page * model.PageSize)
                                     .Take(model.PageSize)
                                     .ToList();

        var allCustomerDto = customerWithPagionation
                            .Select(x => new AllCustomerDto()
                            {
                                Name = x.Name,
                                Surname = x.Surname,
                                ImageUrl = x.ImageUrl,
                                Email = x.Email,
                                Password = x.Password,
                            });

        return allCustomerDto;
    }

    public async Task<AllCustomerDto> GetByIdAsync(int id)
    {
        var customer = await _customerReadRepository.GetByIdAsync(id);

        if (customer is null)
            return null;

        var mappedData = new AllCustomerDto()
        {
            Name = customer.Name,
            Surname = customer.Surname,
            ImageUrl = customer.ImageUrl,
            Email = customer.Email,
            Password = customer.Password,
        };

        return mappedData;
    }

    public async Task<bool> UpdateAsync(int id, UpdateCustomerDto model)
    {
        var customer = await _customerReadRepository.GetByIdAsync(id);

        if (customer is null)
            return false;

        customer.ImageUrl = model.ImageUrl;
        customer.Email = model.Email;
        customer.Password = model.Password;

        await _customerWriteRepository.SaveChangeAsync();

        return true;
    }
}
