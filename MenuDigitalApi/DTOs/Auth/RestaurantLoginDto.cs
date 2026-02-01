using System.ComponentModel.DataAnnotations;

namespace MenuDigitalApi.DTOs.Auth
{
    public class RestaurantLoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
