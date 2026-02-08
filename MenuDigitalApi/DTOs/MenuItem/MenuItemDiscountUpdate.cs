using System.ComponentModel.DataAnnotations;

namespace MenuDigitalApi.DTOs.MenuItem
{
    public class MenuItemDiscountUpdateDto
    {
        [Range(0, 100)]
        public decimal? DiscountPercentage { get; set; }
    }
}