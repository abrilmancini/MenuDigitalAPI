using MenuDigitalApi.Models;

namespace MenuDigitalApi.Repositories.Interfaces
{
    public interface IMenuCategoryRepository
    {
        Task<IEnumerable<MenuCategory>> GetAllAsync();
        Task<MenuCategory?> GetByIdAsync(int id);
        Task<MenuCategory> AddAsync(MenuCategory category);
        Task<MenuCategory> UpdateAsync(MenuCategory category);
        Task DeleteAsync(int id);
    }
}

