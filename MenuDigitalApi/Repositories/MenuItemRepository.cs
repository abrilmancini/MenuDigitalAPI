using MenuDigitalApi.Data;
using MenuDigitalApi.Models;
using MenuDigitalApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MenuDigitalApi.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly AppDbContext _context;

        public MenuItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MenuItem>> GetAllAsync()
        {
            return await _context.MenuItems
                .Include(i => i.MenuCategory)
                .ToListAsync();
        }
        public async Task<IEnumerable<MenuItem>> GetByRestaurantIdAsync(int restaurantId)
        {
            return await _context.MenuItems
                .Include(i => i.MenuCategory)
                .Where(i => i.MenuCategory.RestaurantId == restaurantId)
                .ToListAsync();
        }

        public async Task<IEnumerable<MenuItem>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.MenuItems
                .Include(i => i.MenuCategory)
                .Where(i => i.MenuCategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<MenuItem>> GetFeaturedByRestaurantIdAsync(int restaurantId)
        {
            return await _context.MenuItems
                .Include(i => i.MenuCategory)
                .Where(i => i.MenuCategory.RestaurantId == restaurantId && i.IsFeatured)
                .ToListAsync();
        }

        public async Task<IEnumerable<MenuItem>> GetDiscountedByRestaurantIdAsync(int restaurantId)
        {
            return await _context.MenuItems
                .Include(i => i.MenuCategory)
                .Where(i => i.MenuCategory.RestaurantId == restaurantId && i.DiscountPercentage.HasValue && i.DiscountPercentage > 0)
                .ToListAsync();
        }

        public async Task<IEnumerable<MenuItem>> GetHappyHourByRestaurantIdAsync(int restaurantId)
        {
            return await _context.MenuItems
                .Include(i => i.MenuCategory)
                .Where(i => i.MenuCategory.RestaurantId == restaurantId && i.IsHappyHourEnabled)
                .ToListAsync();
        }
        public async Task<MenuItem?> GetByIdAsync(int id)
        {
            return await _context.MenuItems
                .Include(i => i.MenuCategory)
                .FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<MenuItem?> GetByIdWithOwnershipAsync(int id, int restaurantId)
        {
            return await _context.MenuItems
                .Include(i => i.MenuCategory)
                .FirstOrDefaultAsync(i => i.Id == id && i.MenuCategory.RestaurantId == restaurantId);
        }

        public async Task<MenuItem> AddAsync(MenuItem item)
        {
            _context.MenuItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<MenuItem> UpdateAsync(MenuItem item)
        {
            _context.MenuItems.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.MenuItems.FindAsync(id);
            if (item != null)
            {
                _context.MenuItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
