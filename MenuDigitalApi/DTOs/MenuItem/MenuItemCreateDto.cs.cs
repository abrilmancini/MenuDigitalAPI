namespace MenuDigitalApi.DTOs.MenuItem
{
    public class MenuItemCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int MenuCategoryId { get; set; }
    }
}


