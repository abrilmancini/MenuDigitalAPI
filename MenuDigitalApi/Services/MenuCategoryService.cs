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
            return categories.Select(ToReadDto);
        }

        public async Task<IEnumerable<MenuCategoryReadDto>> GetByRestaurantIdAsync(int restaurantId)
        {
            var categories = await _repository.GetByRestaurantIdAsync(restaurantId);
            return categories.Select(ToReadDto);
        }

        public async Task<MenuCategoryReadDto?> GetByIdAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            return category == null ? null : ToReadDto(category);
        }

        public async Task<MenuCategoryReadDto> CreateAsync(MenuCategoryCreateDto dto, int ownerRestaurantId)
        {
            if (dto.RestaurantId != ownerRestaurantId)
            {
                throw new UnauthorizedAccessException("No podés crear categorías para otro restaurante.");
            }

            var category = new MenuCategory
            {
                Name = dto.Name,
                RestaurantId = dto.RestaurantId
            };

            var created = await _repository.AddAsync(category);
            return ToReadDto(created);
        }
        public async Task UpdateAsync(int id, MenuCategoryUpdateDto dto, int ownerRestaurantId)
        {
            var category = await _repository.GetByIdWithRestaurantAsync(id, ownerRestaurantId);
            if (category == null)
            {
                throw new KeyNotFoundException("Categoría no encontrada para este restaurante");
            }

            if (dto.RestaurantId != ownerRestaurantId)
            {
                throw new UnauthorizedAccessException("No podés mover una categoría a otro restaurante.");
            }

            category.Name = dto.Name;
            category.RestaurantId = dto.RestaurantId;

            await _repository.UpdateAsync(category);
        }

        public async Task DeleteAsync(int id, int ownerRestaurantId)
        {
            var category = await _repository.GetByIdWithRestaurantAsync(id, ownerRestaurantId);
            if (category == null)
            {
                throw new KeyNotFoundException("Categoría no encontrada para este restaurante");
            }

            await _repository.DeleteAsync(id);
        }

        private static MenuCategoryReadDto ToReadDto(MenuCategory category)
        {
            return new MenuCategoryReadDto
            {
                Id = category.Id,
                Name = category.Name,
                RestaurantId = category.RestaurantId
            };
        }
    }
}
