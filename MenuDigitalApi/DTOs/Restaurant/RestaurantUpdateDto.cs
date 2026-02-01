using System.ComponentModel.DataAnnotations;

namespace MenuDigitalApi.DTOs.Restaurant
{
    public class RestaurantUpdateDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = null!;
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; } = null!;
       
    }
}
