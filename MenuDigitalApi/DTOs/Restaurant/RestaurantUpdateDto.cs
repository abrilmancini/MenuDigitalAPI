namespace MenuDigitalApi.DTOs.Restaurant
{
    public class RestaurantUpdateDto
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
    }
}
