
using MenuDigitalApi.DTOs.MenuCategory;


namespace MenuDigitalApi.Services.Interfaces
{
    public interface IMenuCategoryService
    {
        Task<IEnumerable<MenuCategoryReadDto>> GetAllAsync();
        Task<IEnumerable<MenuCategoryReadDto>> GetByRestaurantIdAsync(int restaurantId);
        Task<MenuCategoryReadDto?> GetByIdAsync(int id);
        Task<MenuCategoryReadDto> CreateAsync(MenuCategoryCreateDto dto, int ownerRestaurantId);
        Task UpdateAsync(int id, MenuCategoryUpdateDto dto, int ownerRestaurantId);
        Task DeleteAsync(int id, int ownerRestaurantId);
    }
}
