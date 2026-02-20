using EShop.Application.DTOS.Product;
using EShop.Domain.Entities.Concretes;

namespace EShop.Application.DTOS.Category;

public class AllCategoryDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<AllProductDTO> Products { get; set; }
}
