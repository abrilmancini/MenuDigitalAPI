using MenuDigitalApi.Models;
using MenuDigitalApi.Services.Interfaces;
using MenuDigitalApi.DTOs.MenuCategory;


namespace MenuDigitalApi.Services.Interfaces
{
    public interface IMenuCategoryService
    {
        Task<IEnumerable<MenuCategoryReadDto>> GetAllAsync();
        Task<MenuCategoryReadDto?> GetByIdAsync(int id);
        Task<MenuCategoryReadDto> CreateAsync(MenuCategoryCreateDto dto);
        Task UpdateAsync(int id, MenuCategoryUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
