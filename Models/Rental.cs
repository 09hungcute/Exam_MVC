namespace ComicRentalSystem.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = new Customer(); // Khởi tạo mặc định để tránh null reference
        public DateTime RentalDate { get; set; }

        public ICollection<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>(); // Khởi tạo để tránh null reference
    }
}
