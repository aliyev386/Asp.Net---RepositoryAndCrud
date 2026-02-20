using EShop.Application.DTOS;
using EShop.Application.DTOS.Category;

namespace EShop.Application.Services.Abstracts;

public interface ICategoryService
{
    Task<IEnumerable<AllCategoryDto>> GetAllAsync(PaginationDTO model);
    Task<bool> AddAsync(AddCategoryDto model);
    Task<bool> DeleteAsync(int id);
    Task<AllCategoryDto> GetByIdAsync(int id);
    Task<bool> UpdateAsync(int id, UpdateCagetoryDto model);
}
