using MenuDigitalApi.Data;
using MenuDigitalApi.Models;
using MenuDigitalApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MenuDigitalApi.Repositories
{
    public class MenuCategoryRepository : IMenuCategoryRepository
    {
        private readonly AppDbContext _context;

        public MenuCategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MenuCategory>> GetAllAsync()
        {
            return await _context.MenuCategories
                .Include(c => c.Restaurant)
                .Include(c => c.Items)
                .ToListAsync();
        }

        public async Task<IEnumerable<MenuCategory>> GetByRestaurantIdAsync(int restaurantId)
        {
            return await _context.MenuCategories
                .Include(c => c.Items)
                .Where(c => c.RestaurantId == restaurantId)
                .ToListAsync();
        }
        public async Task<MenuCategory?> GetByIdWithRestaurantAsync(int id, int restaurantId)
        {
            return await _context.MenuCategories
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == id && c.RestaurantId == restaurantId);
        }

        public async Task<MenuCategory?> GetByIdAsync(int id)
        {
            return await _context.MenuCategories
                .Include(c => c.Restaurant)
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<MenuCategory> AddAsync(MenuCategory category)
        {
            _context.MenuCategories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<MenuCategory> UpdateAsync(MenuCategory category)
        {
            _context.MenuCategories.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _context.MenuCategories.FindAsync(id);
            if (category != null)
            {
                _context.MenuCategories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
