using MenuDigitalApi.Models;

namespace MenuDigitalApi.Repositories.Interfaces
{
    public interface IMenuItemRepository
    {
        Task<IEnumerable<MenuItem>> GetAllAsync();
        Task<IEnumerable<MenuItem>> GetByRestaurantIdAsync(int restaurantId);
        Task<IEnumerable<MenuItem>> GetByCategoryIdAsync(int categoryId);
        Task<IEnumerable<MenuItem>> GetFeaturedByRestaurantIdAsync(int restaurantId);
        Task<IEnumerable<MenuItem>> GetDiscountedByRestaurantIdAsync(int restaurantId);
        Task<IEnumerable<MenuItem>> GetHappyHourByRestaurantIdAsync(int restaurantId);
        Task<MenuItem?> GetByIdAsync(int id);
        Task<MenuItem?> GetByIdWithOwnershipAsync(int id, int restaurantId);
        Task<MenuItem> AddAsync(MenuItem item);
        Task<MenuItem> UpdateAsync(MenuItem item);
        Task DeleteAsync(int id);
    }
}

