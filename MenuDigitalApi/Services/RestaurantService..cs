using MenuDigitalApi.DTOs.Restaurant;
using MenuDigitalApi.Models;
using MenuDigitalApi.Repositories.Interfaces;
using MenuDigitalApi.Services.Interfaces;
using BCrypt.Net;


namespace MenuDigitalApi.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _repository;

        public RestaurantService(IRestaurantRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RestaurantReadDto>> GetAllAsync()
        {
            var restaurants = await _repository.GetAllAsync();

            return restaurants.Select(r => new RestaurantReadDto
            {
                Id = r.Id,
                Name = r.Name,
                Email = r.Email
            });
        }

        public async Task<RestaurantReadDto?> GetByIdAsync(int id)
        {
            var restaurant = await _repository.GetByIdAsync(id);
            if (restaurant == null)
                return null;

            return new RestaurantReadDto
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Email = restaurant.Email
            };
        }

        public async Task<RestaurantReadDto> CreateAsync(RestaurantCreateDto dto)
        {
            var restaurant = new Restaurant
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.PasswordHash)

            };

            var created = await _repository.AddAsync(restaurant);

            return new RestaurantReadDto
            {
                Id = created.Id,
                Name = created.Name,
                Email = created.Email
            };
        }

        public async Task UpdateAsync(int id, RestaurantUpdateDto dto)
        {
            var restaurant = await _repository.GetByIdAsync(id);
            if (restaurant == null)
                throw new KeyNotFoundException("Restaurante no encontrado");

            restaurant.Name = dto.Name;
            restaurant.Email = dto.Email;

            await _repository.UpdateAsync(restaurant);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
        public async Task<Restaurant?> GetByEmailAsync(string email)
        {
            return await _repository.GetByEmailAsync(email);
        }
    }
}
