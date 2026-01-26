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
                .Include(c => c.Restaurant) // Incluye el restaurante asociado
                .Include(c => c.Items)      // Incluye los ítems de la categoría
                .ToListAsync();
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
