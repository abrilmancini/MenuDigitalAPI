namespace MenuDigitalApi.DTOs.Restaurant
{
    public class RestaurantCreateDto
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!; // el hash se genera en backend
    }
}
