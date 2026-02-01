using MenuDigitalApi.DTOs.Auth;
using MenuDigitalApi.DTOs.Restaurant;
using MenuDigitalApi.Models;
using MenuDigitalApi.Repositories;
using MenuDigitalApi.Repositories.Interfaces;
using MenuDigitalApi.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace MenuDigitalApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IConfiguration _configuration;
        public AuthService(IRestaurantRepository restaurantRepository,IConfiguration configuration)
        {
            _restaurantRepository = restaurantRepository;
            _configuration = configuration;
        }

        public async Task<string?> AuthenticateRestaurantAsync(RestaurantLoginDto dto)
        {
            var restaurant = await _restaurantRepository.GetByEmailAsync(dto.Email);
            if (restaurant == null || !BCrypt.Net.BCrypt.Verify(dto.Password, restaurant.PasswordHash))
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, restaurant.Id.ToString()),
                    new Claim(ClaimTypes.Email, restaurant.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }



        public async Task<string?> LoginAsync(string email, string password)
        {
            // 1. Buscar restaurante por email
            var restaurant = await _restaurantRepository.GetByEmailAsync(email);
            if (restaurant == null)
                return null;

            // 2. Verificar password
            var validPassword = BCrypt.Net.BCrypt.Verify(password, restaurant.PasswordHash);
            if (!validPassword)
                return null;

            // 3. Crear JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(
                _configuration["Jwt:Key"]
                ?? throw new InvalidOperationException("JWT Key not configured")
            );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, restaurant.Id.ToString()),
            new Claim(ClaimTypes.Email, restaurant.Email)
        }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<RestaurantReadDto?> RegisterAsync(RestaurantCreateDto dto)
        {
            var existing = await _restaurantRepository.GetByEmailAsync(dto.Email);
            if (existing != null)
                return null;

            var restaurant = new Restaurant
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.PasswordHash)
            };

            var created = await _restaurantRepository.AddAsync(restaurant);

            return new RestaurantReadDto
            {
                Id = created.Id,
                Name = created.Name,
                Email = created.Email
            };
        }

    }
}
