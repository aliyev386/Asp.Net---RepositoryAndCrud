using EShop.Application.DTOS;
using EShop.Application.Repositories;
using EShop.Domain.Entities.Concretes;
using EShop.Application.DTOS.Category;
using EShop.Application.Services.Abstracts;

namespace EShop.Persistence.Services.Concretes;

public class CategoryService : ICategoryService
{
    private readonly ICategoryReadRepository _categoryReadRepository;
    private readonly ICategoryWriteRepository _categoryWriteRepository;

    public CategoryService(ICategoryReadRepository categoryReadRepository, ICategoryWriteRepository categoryWriteRepository)
    {
        _categoryReadRepository = categoryReadRepository;
        _categoryWriteRepository = categoryWriteRepository;
    }


    public async Task<bool> AddAsync(AddCategoryDto model)
    {
        var newCategory = new Category()
        {
            Name = model.Name,
            Description = model.Description
        };

        await _categoryWriteRepository.AddAsync(newCategory);
        await _categoryWriteRepository.SaveChangeAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _categoryReadRepository.GetByIdAsync(id);

        if (category is null)
            return false;

        await _categoryWriteRepository.Delete(id);
        await _categoryWriteRepository.SaveChangeAsync();
        return true;
    }

    public async Task<IEnumerable<AllCategoryDto>> GetAllAsync(PaginationDTO model)
    {
        var categories = await _categoryReadRepository.GetAllAsync();

        var categoryWithPagination = categories
                                     .Skip(model.Page * model.PageSize)
                                     .Take(model.PageSize)
                                     .ToList();

        var allCategoryDto = categoryWithPagination
                            .Select(x => new AllCategoryDto()
                            {
                                Name = x.Name,
                                Description = x.Description
                            });

        return allCategoryDto;
    }

    public async Task<AllCategoryDto> GetByIdAsync(int id)
    {
        var category = await _categoryReadRepository.GetByIdAsync(id);

        if (category is null)
            return null;

        var mappedData = new AllCategoryDto()
        {
            Name = category.Name,
            Description = category.Description
        };
        return mappedData;
    }

    public async Task<bool> UpdateAsync(int id, UpdateCagetoryDto model)
    {
        var category = await _categoryReadRepository.GetByIdAsync(id);

        if (category is null)
            return false;

        category.Name = model.Name;
        category.Description = model.Description;

        await _categoryWriteRepository.SaveChangeAsync();

        return true;
    }

}
