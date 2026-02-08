namespace MenuDigitalApi.Models
{
    public class MenuItem
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int MenuCategoryId { get; set; }
        public MenuCategory MenuCategory { get; set; } = null!;

        public bool IsAvailable { get; set; } = true;
        public bool IsFeatured { get; set; }

        public decimal? DiscountPercentage { get; set; }

        public bool IsHappyHourEnabled { get; set; }
        public decimal? HappyHourPrice { get; set; }
        public TimeSpan? HappyHourStart { get; set; }
        public TimeSpan? HappyHourEnd { get; set; }
    }
}