using MenuDigitalApi.DTOs.MenuItem;
using MenuDigitalApi.Models;
using MenuDigitalApi.Repositories.Interfaces;
using MenuDigitalApi.Services.Interfaces;

namespace MenuDigitalApi.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuItemRepository _repository;
        private readonly IMenuCategoryRepository _categoryRepository;

        public MenuItemService(
            IMenuItemRepository repository,
            IMenuCategoryRepository categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }

        /* =======================
           Helpers
        ======================== */

        private bool IsHappyHourNow(MenuItem item)
        {
            if (!item.IsHappyHourEnabled)
                return false;

            if (!item.HappyHourStart.HasValue || !item.HappyHourEnd.HasValue)
                return false;

            var now = DateTime.Now.TimeOfDay;
            return now >= item.HappyHourStart.Value &&
                   now <= item.HappyHourEnd.Value;
        }

        private decimal GetFinalPrice(MenuItem item)
        {
            var price = item.Price;

            if (IsHappyHourNow(item) && item.HappyHourPrice.HasValue)
                price = item.HappyHourPrice.Value;

            if (item.DiscountPercentage.HasValue && item.DiscountPercentage > 0)
                price -= price * item.DiscountPercentage.Value / 100m;

            return price;
        }

        private MenuItemReadDto ToReadDto(MenuItem item)
        {
            return new MenuItemReadDto
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                FinalPrice = GetFinalPrice(item),
                IsAvailable = item.IsAvailable,
                IsFeatured = item.IsFeatured,
                IsHappyHour = IsHappyHourNow(item),
                DiscountPercentage = item.DiscountPercentage,
                MenuCategoryId = item.MenuCategoryId
            };
        }

        /* =======================
           Reads
        ======================== */

        public async Task<IEnumerable<MenuItemReadDto>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();
            return items.Select(ToReadDto);
        }

        public async Task<MenuItemReadDto?> GetByIdAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            return item == null ? null : ToReadDto(item);
        }

        public async Task<IEnumerable<MenuItemReadDto>> GetByRestaurantIdAsync(int restaurantId)
        {
            var items = await _repository.GetByRestaurantIdAsync(restaurantId);
            return items.Select(ToReadDto);
        }

        public async Task<IEnumerable<MenuItemReadDto>> GetByCategoryIdAsync(int categoryId)
        {
            var items = await _repository.GetByCategoryIdAsync(categoryId);
            return items.Select(ToReadDto);
        }

        public async Task<IEnumerable<MenuItemReadDto>> GetFeaturedByRestaurantIdAsync(int restaurantId)
        {
            var items = await _repository.GetFeaturedByRestaurantIdAsync(restaurantId);
            return items.Select(ToReadDto);
        }

        public async Task<IEnumerable<MenuItemReadDto>> GetDiscountedByRestaurantIdAsync(int restaurantId)
        {
            var items = await _repository.GetDiscountedByRestaurantIdAsync(restaurantId);
            return items.Select(ToReadDto);
        }

        public async Task<IEnumerable<MenuItemReadDto>> GetHappyHourByRestaurantIdAsync(int restaurantId)
        {
            var items = await _repository.GetHappyHourByRestaurantIdAsync(restaurantId);
            return items.Select(ToReadDto);
        }

        /* =======================
           Create
        ======================== */

        public async Task<MenuItemReadDto> CreateAsync(
            MenuItemCreateDto dto,
            int ownerRestaurantId)
        {
            var category = await _categoryRepository
                .GetByIdWithRestaurantAsync(dto.MenuCategoryId, ownerRestaurantId);

            if (category == null)
                throw new UnauthorizedAccessException(
                    "No podés crear productos en una categoría de otro restaurante.");

            var item = new MenuItem
            {
                Name = dto.Name,
                Description = dto.Description ?? string.Empty,
                Price = dto.Price,
                IsAvailable = dto.IsAvailable,
                IsFeatured = dto.IsFeatured,
                MenuCategoryId = dto.MenuCategoryId,
                DiscountPercentage = dto.DiscountPercentage,
                HappyHourPrice = dto.HappyHourPrice,
                HappyHourStart = dto.HappyHourStart,
                HappyHourEnd = dto.HappyHourEnd,
                IsHappyHourEnabled = dto.IsHappyHourEnabled
            };

            var created = await _repository.AddAsync(item);
            return ToReadDto(created);
        }

        /* =======================
           Update
        ======================== */

        public async Task<MenuItemReadDto> UpdateAsync(
            int id,
            MenuItemUpdateDto dto,
            int ownerRestaurantId)
        {
            var item = await _repository
                .GetByIdWithOwnershipAsync(id, ownerRestaurantId);

            if (item == null)
                throw new KeyNotFoundException(
                    "MenuItem no encontrado para este restaurante");

            var category = await _categoryRepository
                .GetByIdWithRestaurantAsync(dto.MenuCategoryId, ownerRestaurantId);

            if (category == null)
                throw new UnauthorizedAccessException(
                    "No podés mover productos a una categoría de otro restaurante.");

            item.Name = dto.Name;
            item.Description = dto.Description ?? string.Empty;
            item.Price = dto.Price;
            item.IsAvailable = dto.IsAvailable;
            item.IsFeatured = dto.IsFeatured;
            item.MenuCategoryId = dto.MenuCategoryId;
            item.DiscountPercentage = dto.DiscountPercentage;
            item.HappyHourPrice = dto.HappyHourPrice;
            item.HappyHourStart = dto.HappyHourStart;
            item.HappyHourEnd = dto.HappyHourEnd;
            item.IsHappyHourEnabled = dto.IsHappyHourEnabled;

            await _repository.UpdateAsync(item);
            return ToReadDto(item);
        }

        public async Task<MenuItemReadDto> UpdateDiscountAsync(
            int id,
            decimal? discountPercentage,
            int ownerRestaurantId)
        {
            var item = await _repository
                .GetByIdWithOwnershipAsync(id, ownerRestaurantId);

            if (item == null)
                throw new KeyNotFoundException(
                    "MenuItem no encontrado para este restaurante");

            item.DiscountPercentage = discountPercentage;
            await _repository.UpdateAsync(item);
            return ToReadDto(item);
        }

        public async Task<MenuItemReadDto> ToggleHappyHourAsync(
            int id,
            bool enabled,
            int ownerRestaurantId)
        {
            var item = await _repository
                .GetByIdWithOwnershipAsync(id, ownerRestaurantId);

            if (item == null)
                throw new KeyNotFoundException(
                    "MenuItem no encontrado para este restaurante");

            item.IsHappyHourEnabled = enabled;
            await _repository.UpdateAsync(item);
            return ToReadDto(item);
        }

        /* =======================
           Delete
        ======================== */

        public async Task DeleteAsync(int id, int ownerRestaurantId)
        {
            var item = await _repository
                .GetByIdWithOwnershipAsync(id, ownerRestaurantId);

            if (item == null)
                throw new KeyNotFoundException(
                    "MenuItem no encontrado para este restaurante");

            await _repository.DeleteAsync(id);
        }
    }
}
