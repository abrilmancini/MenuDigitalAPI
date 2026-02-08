using MenuDigitalApi.Models;

namespace MenuDigitalApi.Repositories.Interfaces
{
    public interface IMenuCategoryRepository
    {
        Task<IEnumerable<MenuCategory>> GetAllAsync();
        Task<IEnumerable<MenuCategory>> GetByRestaurantIdAsync(int restaurantId);
        Task<MenuCategory?> GetByIdAsync(int id);
        Task<MenuCategory?> GetByIdWithRestaurantAsync(int id, int restaurantId);
        Task<MenuCategory> AddAsync(MenuCategory category);
        Task<MenuCategory> UpdateAsync(MenuCategory category);
        Task DeleteAsync(int id);
    }
}

