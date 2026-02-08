using MenuDigitalApi.DTOs.Restaurant;
using MenuDigitalApi.Models;

namespace MenuDigitalApi.Services.Interfaces
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantReadDto>> GetAllAsync();
        Task<RestaurantReadDto?> GetByIdAsync(int id);
        Task<RestaurantReadDto> CreateAsync(RestaurantCreateDto dto);
        Task<Restaurant?> GetByEmailAsync(string email);
        Task UpdateAsync(int id, RestaurantUpdateDto dto);
        Task DeleteAsync(int id);
        Task UpdateOwnAsync(int ownerRestaurantId, RestaurantUpdateDto dto);
        Task DeleteOwnAsync(int ownerRestaurantId);
    }
}