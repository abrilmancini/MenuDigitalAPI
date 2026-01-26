namespace MenuDigitalApi.DTOs.MenuCategory
{
    public class MenuCategoryUpdateDto
    {
        public string Name { get; set; } = null!;
        public int RestaurantId { get; set; }
    }
}
