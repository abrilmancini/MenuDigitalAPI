namespace MenuDigitalApi.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }=null!;
        public string Description { get; set; }=null!;
        public decimal Price { get; set; }
        public int MenuCategoryId { get; set; }
        public bool IsAvailable { get; set; } = true;
        public MenuCategory MenuCategory { get; set; }=null!;
        public decimal? HappyHourPrice { get; set; }
        public TimeSpan? HappyHourStart { get; set; }
        public TimeSpan? HappyHourEnd { get; set; }

    }
}
