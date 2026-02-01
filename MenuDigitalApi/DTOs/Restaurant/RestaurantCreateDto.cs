using System.ComponentModel.DataAnnotations;

namespace MenuDigitalApi.DTOs.Restaurant
{
    public class RestaurantCreateDto
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = null!;
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; } = null!;
        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string PasswordHash { get; set; } = null!; 
    }
}
