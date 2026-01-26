using MenuDigitalApi.DTOs.MenuCategory;
using MenuDigitalApi.Models;
using MenuDigitalApi.Repositories.Interfaces;
using MenuDigitalApi.Services.Interfaces;

namespace MenuDigitalApi.Services
{
    public class MenuCategoryService : IMenuCategoryService
    {
        private readonly IMenuCategoryRepository _repository;

        public MenuCategoryService(IMenuCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MenuCategoryReadDto>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();
            return categories.Select(c => new MenuCategoryReadDto
            {
                Id = c.Id,
                Name = c.Name,
                RestaurantId = c.RestaurantId
            });
        }

        public async Task<MenuCategoryReadDto?> GetByIdAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
                return null;

            return new MenuCategoryReadDto
            {
                Id = category.Id,
                Name = category.Name,
                RestaurantId = category.RestaurantId
            };
        }

        public async Task<MenuCategoryReadDto> CreateAsync(MenuCategoryCreateDto dto)
        {
            var category = new MenuCategory
            {
                Name = dto.Name,
                RestaurantId = dto.RestaurantId
            };

            var created = await _repository.AddAsync(category);

            return new MenuCategoryReadDto
            {
                Id = created.Id,
                Name = created.Name,
                RestaurantId = created.RestaurantId
            };
        }

        public async Task UpdateAsync(int id, MenuCategoryUpdateDto dto)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
                throw new KeyNotFoundException("Categoría no encontrada");

            category.Name = dto.Name;
            category.RestaurantId = dto.RestaurantId;

            await _repository.UpdateAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
