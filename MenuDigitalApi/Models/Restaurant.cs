
namespace MenuDigitalApi.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }=null!;
        public string Email { get; set; }=null!;
        public string PasswordHash { get; set; } = null!;
        public int visitCount { get; set; } = 0;
        public ICollection<MenuCategory> Categories { get; set; } = new List<MenuCategory>();

    }
}
