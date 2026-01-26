using MenuDigitalApi.DTOs.MenuItem;

namespace MenuDigitalApi.Services.Interfaces
{
    public interface IMenuItemService
    {
        Task<IEnumerable<MenuItemReadDto>> GetAllAsync();
        Task<MenuItemReadDto?> GetByIdAsync(int id);
        Task<MenuItemReadDto> CreateAsync(MenuItemCreateDto dto);
        Task<MenuItemReadDto> UpdateAsync(int id, MenuItemUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
