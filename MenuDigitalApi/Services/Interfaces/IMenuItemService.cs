using MenuDigitalApi.DTOs.MenuItem;

namespace MenuDigitalApi.Services.Interfaces
{
    public interface IMenuItemService
    {
        Task<IEnumerable<MenuItemReadDto>> GetAllAsync();
        Task<IEnumerable<MenuItemReadDto>> GetByRestaurantIdAsync(int restaurantId);
        Task<IEnumerable<MenuItemReadDto>> GetByCategoryIdAsync(int categoryId);
        Task<IEnumerable<MenuItemReadDto>> GetFeaturedByRestaurantIdAsync(int restaurantId);
        Task<IEnumerable<MenuItemReadDto>> GetDiscountedByRestaurantIdAsync(int restaurantId);
        Task<IEnumerable<MenuItemReadDto>> GetHappyHourByRestaurantIdAsync(int restaurantId);

        Task<MenuItemReadDto?> GetByIdAsync(int id);

        Task<MenuItemReadDto> CreateAsync(
            MenuItemCreateDto dto,
            int ownerRestaurantId);

        Task<MenuItemReadDto> UpdateAsync(
            int id,
            MenuItemUpdateDto dto,
            int ownerRestaurantId);

        Task<MenuItemReadDto> UpdateDiscountAsync(
            int id,
            decimal? discountPercentage,
            int ownerRestaurantId);

        Task<MenuItemReadDto> ToggleHappyHourAsync(
            int id,
            bool enabled,
            int ownerRestaurantId);

        Task DeleteAsync(
            int id,
            int ownerRestaurantId);
    }
}