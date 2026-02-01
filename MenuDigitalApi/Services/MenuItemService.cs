using MenuDigitalApi.DTOs.MenuItem;
using MenuDigitalApi.Models;
using MenuDigitalApi.Repositories.Interfaces;
using MenuDigitalApi.Services.Interfaces;

namespace MenuDigitalApi.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuItemRepository _repository;
        private decimal GetFinalPrice(MenuItem item)
        {
            if (
                item.HappyHourPrice.HasValue &&
                item.HappyHourStart.HasValue &&
                item.HappyHourEnd.HasValue
            )
            {
                var now = DateTime.Now.TimeOfDay;

                if (now >= item.HappyHourStart && now <= item.HappyHourEnd)
                {
                    return item.HappyHourPrice.Value;
                }
            }

            return item.Price;
        }

        public MenuItemService(IMenuItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MenuItemReadDto>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();

            return items.Select(i => new MenuItemReadDto
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                Price = GetFinalPrice(i),
                IsAvailable = i.IsAvailable,
                MenuCategoryId = i.MenuCategoryId
            });
        }

        public async Task<MenuItemReadDto?> GetByIdAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
                return null;

            return new MenuItemReadDto
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = GetFinalPrice(item),
                IsAvailable = item.IsAvailable,
                MenuCategoryId = item.MenuCategoryId
            };
        }

        public async Task<MenuItemReadDto> CreateAsync(MenuItemCreateDto dto)
        {
            var item = new MenuItem
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                IsAvailable = dto.IsAvailable,
                MenuCategoryId = dto.MenuCategoryId
            };

            var created = await _repository.AddAsync(item);

            return new MenuItemReadDto
            {
                Id = created.Id,
                Name = created.Name,
                Description = created.Description,
                Price = created.Price,
                IsAvailable = created.IsAvailable,
                MenuCategoryId = created.MenuCategoryId
            };
        }

        // 🔧 MÉTODO CORREGIDO
        public async Task<MenuItemReadDto> UpdateAsync(int id, MenuItemUpdateDto dto)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
                throw new KeyNotFoundException("MenuItem no encontrado");

            item.Name = dto.Name;
            item.Description = dto.Description;
            item.Price = dto.Price;
            item.IsAvailable = dto.IsAvailable;
            item.MenuCategoryId = dto.MenuCategoryId;

            await _repository.UpdateAsync(item);

            return new MenuItemReadDto
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                IsAvailable = item.IsAvailable,
                MenuCategoryId = item.MenuCategoryId
            };
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
       

    }
}
