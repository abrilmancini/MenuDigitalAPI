namespace MenuDigitalApi.Models
{
    public class MenuCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }=null!;
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }=null!;
        public ICollection<MenuItem> Items { get; set; } = new List<MenuItem>();
    }
}
