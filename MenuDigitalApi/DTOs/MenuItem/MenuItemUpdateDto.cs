namespace MenuDigitalApi.DTOs.MenuItem
{
    public class MenuItemUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public int MenuCategoryId { get; set; }
    }
}
