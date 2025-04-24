namespace ComicRentalSystem.Models
{
    public class RentalDetail
    {
        public int Id { get; set; }
        public int RentalId { get; set; }
        public Rental Rental { get; set; } = new Rental(); // Khởi tạo mặc định để tránh null reference

        public int ComicBookId { get; set; }
        public ComicBook ComicBook { get; set; } = new ComicBook(); // Khởi tạo mặc định để tránh null reference

        public int Quantity { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal PricePerDay { get; set; }
    }
}
