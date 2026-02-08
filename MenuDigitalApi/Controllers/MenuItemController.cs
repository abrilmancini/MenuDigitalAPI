using MenuDigitalApi.DTOs.MenuItem;
using MenuDigitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace MenuDigitalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService _service;

        public MenuItemController(IMenuItemService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItemReadDto>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<MenuItemReadDto>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpGet("restaurant/{restaurantId:int}")]
        public async Task<ActionResult<IEnumerable<MenuItemReadDto>>> GetByRestaurantId(int restaurantId)
        {
            var result = await _service.GetByRestaurantIdAsync(restaurantId);
            return Ok(result);
        }

        [HttpGet("category/{categoryId:int}")]
        public async Task<ActionResult<IEnumerable<MenuItemReadDto>>> GetByCategoryId(int categoryId)
        {
            var result = await _service.GetByCategoryIdAsync(categoryId);
            return Ok(result);
        }

        [HttpGet("restaurant/{restaurantId:int}/featured")]
        public async Task<ActionResult<IEnumerable<MenuItemReadDto>>> GetFeaturedByRestaurantId(int restaurantId)
        {
            var result = await _service.GetFeaturedByRestaurantIdAsync(restaurantId);
            return Ok(result);
        }

        [HttpGet("restaurant/{restaurantId:int}/discounted")]
        public async Task<ActionResult<IEnumerable<MenuItemReadDto>>> GetDiscountedByRestaurantId(int restaurantId)
        {
            var result = await _service.GetDiscountedByRestaurantIdAsync(restaurantId);
            return Ok(result);
        }

        [HttpGet("restaurant/{restaurantId:int}/happy-hour")]
        public async Task<ActionResult<IEnumerable<MenuItemReadDto>>> GetHappyHourByRestaurantId(int restaurantId)
        {
            var result = await _service.GetHappyHourByRestaurantIdAsync(restaurantId);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<MenuItemReadDto>> Create([FromBody] MenuItemCreateDto dto)
        {
            var ownerRestaurantId = GetOwnerRestaurantId();
            var created = await _service.CreateAsync(dto, ownerRestaurantId);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] MenuItemUpdateDto dto)
        {
            try
            {
                var ownerRestaurantId = GetOwnerRestaurantId();
                await _service.UpdateAsync(id, dto, ownerRestaurantId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(StatusCodes.Status403Forbidden, ex.Message);
            }
        }

        [Authorize]
        [HttpPatch("{id:int}/discount")]
        public async Task<ActionResult<MenuItemReadDto>> UpdateDiscount(int id, [FromBody] MenuItemDiscountUpdateDto dto)
        {
            try
            {
                var ownerRestaurantId = GetOwnerRestaurantId();
                var updated = await _service.UpdateDiscountAsync(id, dto.DiscountPercentage, ownerRestaurantId);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpPatch("{id:int}/happy-hour")]
        public async Task<ActionResult<MenuItemReadDto>> ToggleHappyHour(int id, [FromBody] MenuItemHappyHourToggleDto dto)
        {
            try
            {
                var ownerRestaurantId = GetOwnerRestaurantId();
                var updated = await _service.ToggleHappyHourAsync(id, dto.Enabled, ownerRestaurantId);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var ownerRestaurantId = GetOwnerRestaurantId();
                await _service.DeleteAsync(id, ownerRestaurantId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        private int GetOwnerRestaurantId()
        {
            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(claim, out var ownerRestaurantId))
            {
                throw new UnauthorizedAccessException("Token inválido.");
            }

            return ownerRestaurantId;
        }
    }
}
