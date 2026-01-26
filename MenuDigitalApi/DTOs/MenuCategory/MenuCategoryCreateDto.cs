namespace MenuDigitalApi.DTOs.MenuCategory
{
    public class MenuCategoryCreateDto
    {
        public string Name { get; set; } = null!;
        public int RestaurantId { get; set; }
    }
}
