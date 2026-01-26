namespace MenuDigitalApi.DTOs.MenuCategory
{
    public class MenuCategoryReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int RestaurantId { get; set; }
    }
}
