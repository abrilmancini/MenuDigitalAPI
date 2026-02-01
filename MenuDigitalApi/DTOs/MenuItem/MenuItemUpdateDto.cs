using System.ComponentModel.DataAnnotations;

namespace MenuDigitalApi.DTOs.MenuItem
{
    public class MenuItemUpdateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? Description { get; set; }
        [Range(0.01, 10000.00)]
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        [Range(1, int.MaxValue)]
        public int MenuCategoryId { get; set; }
        public TimeSpan? HappyHourStart { get; set; }
        public TimeSpan? HappyHourEnd { get; set; }
        public decimal? HappyHourPrice { get; set; }

    }
}
