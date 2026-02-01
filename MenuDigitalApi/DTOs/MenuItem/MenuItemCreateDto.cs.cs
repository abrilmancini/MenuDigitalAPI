using System.ComponentModel.DataAnnotations;

namespace MenuDigitalApi.DTOs.MenuItem
{
    public class MenuItemCreateDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? Description { get; set; }
        [Range(0.01, 10000.00, ErrorMessage = "Price must be between 0.01 and 10,000.00.")]
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; } = true;
        [Range(1, int.MaxValue, ErrorMessage = "MenuCategoryId must be a positive integer.")]
        public int MenuCategoryId { get; set; }
        public decimal? HappyHourPrice { get; set; }
        public TimeSpan? HappyHourStart { get; set; }
        public TimeSpan? HappyHourEnd { get; set; }

    }
}


