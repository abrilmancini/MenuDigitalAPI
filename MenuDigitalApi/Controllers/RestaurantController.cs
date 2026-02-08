using MenuDigitalApi.DTOs.Restaurant;
using MenuDigitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MenuDigitalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _service;

        public RestaurantController(IRestaurantService service)
        {
            _service = service;
        }

        // =======================
        // GET
        // =======================

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantReadDto>>> GetAll()
        {
            var restaurants = await _service.GetAllAsync();
            return Ok(restaurants);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<RestaurantReadDto>> GetById(int id)
        {
            var restaurant = await _service.GetByIdAsync(id);
            if (restaurant == null)
                return NotFound();

            return Ok(restaurant);
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<RestaurantReadDto>> GetMe()
        {
            var ownerRestaurantId = GetOwnerRestaurantId();
            var restaurant = await _service.GetByIdAsync(ownerRestaurantId);

            if (restaurant == null)
                return NotFound();

            return Ok(restaurant);
        }

        // =======================
        // POST
        // =======================

        [HttpPost]
        public async Task<ActionResult<RestaurantReadDto>> Create(RestaurantCreateDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // =======================
        // PUT
        // =======================

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, RestaurantUpdateDto dto)
        {
            try
            {
                await _service.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpPut("me")]
        public async Task<IActionResult> UpdateMine(RestaurantUpdateDto dto)
        {
            try
            {
                var ownerRestaurantId = GetOwnerRestaurantId();
                await _service.UpdateOwnAsync(ownerRestaurantId, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // =======================
        // DELETE
        // =======================

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpDelete("me")]
        public async Task<IActionResult> DeleteMine()
        {
            try
            {
                var ownerRestaurantId = GetOwnerRestaurantId();
                await _service.DeleteOwnAsync(ownerRestaurantId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // =======================
        // Helper
        // =======================

        private int GetOwnerRestaurantId()
        {
            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(claim, out var ownerRestaurantId))
                throw new UnauthorizedAccessException("Token inválido");

            return ownerRestaurantId;
        }
    }
}
