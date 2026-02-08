using MenuDigitalApi.DTOs.Auth;
using MenuDigitalApi.DTOs.Restaurant;
using MenuDigitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MenuDigitalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] RestaurantLoginDto dto)
        {
            var token = await _authService.LoginAsync(dto.Email, dto.Password);

            if (token == null)
                return Unauthorized("Email o contraseña incorrectos");

            return Ok(new { token });
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RestaurantCreateDto dto)
        {
            var result = await _authService.RegisterAsync(dto);

            if (result == null)
                return BadRequest("No se pudo registrar el restaurante");

            return Ok(result);
        }

    }
}
