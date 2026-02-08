using MenuDigitalApi.DTOs.MenuCategory;
using MenuDigitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MenuDigitalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuCategoryController : ControllerBase
    {
        private readonly IMenuCategoryService _service;

        public MenuCategoryController(IMenuCategoryService service)
        {
            _service = service;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuCategoryReadDto>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("restaurant/{restaurantId:int}")]
        public async Task<ActionResult<IEnumerable<MenuCategoryReadDto>>> GetByRestaurant(int restaurantId)
        {
            var result = await _service.GetByRestaurantIdAsync(restaurantId);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MenuCategoryReadDto>> GetById(int id)
        {
            var category = await _service.GetByIdAsync(id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<MenuCategoryReadDto>> Create([FromBody] MenuCategoryCreateDto dto)
        {
            try
            {
                var ownerRestaurantId = GetOwnerRestaurantId();
                var created = await _service.CreateAsync(dto, ownerRestaurantId);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(StatusCodes.Status403Forbidden, ex.Message);
            }
        }

        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] MenuCategoryUpdateDto dto)
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
