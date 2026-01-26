
using MenuDigitalApi.DTOs.Restaurant;

namespace MenuDigitalApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<RestaurantReadDto?> RegisterAsync(RestaurantCreateDto dto);
        Task<string?> LoginAsync(string email, string password);
    }
}
