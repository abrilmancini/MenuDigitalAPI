namespace MenuDigitalApi.DTOs.MenuItem
{
    public class MenuItemReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public int MenuCategoryId { get; set; }
        public decimal FinalPrice { get; set; }
        public bool IsHappyHour { get; set; }

    }
}
