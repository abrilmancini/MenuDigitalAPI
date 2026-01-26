using MenuDigitalApi.DTOs.MenuCategory;
using MenuDigitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        // GET api/MenuCategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuCategoryReadDto>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // GET api/MenuCategory/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<MenuCategoryReadDto>> GetById(int id)
        {
            var category = await _service.GetByIdAsync(id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        // POST api/MenuCategory
        [HttpPost]
        public async Task<ActionResult<MenuCategoryReadDto>> Create(MenuCategoryCreateDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT api/MenuCategory/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, MenuCategoryUpdateDto dto)
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

        // DELETE api/MenuCategory/5
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
    }
}
