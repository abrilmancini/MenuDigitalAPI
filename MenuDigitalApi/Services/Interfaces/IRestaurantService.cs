using MenuDigitalApi.DTOs.Restaurant;

namespace MenuDigitalApi.Services.Interfaces
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantReadDto>> GetAllAsync();
        Task<RestaurantReadDto?> GetByIdAsync(int id);
        Task<RestaurantReadDto> CreateAsync(RestaurantCreateDto dto);
        Task UpdateAsync(int id, RestaurantUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
