namespace MenuDigitalApi.DTOs.Restaurant
{
    public class RestaurantReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int VisitCount { get; set; }
    }
}
