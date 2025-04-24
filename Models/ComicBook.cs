namespace ComicRentalSystem.Models
{
    public class ComicBook
    {
        public int Id { get; set; }
        public string BookName { get; set; } = string.Empty; // Khởi tạo mặc định để tránh null reference
        public string Author { get; set; } = string.Empty; // Khởi tạo mặc định để tránh null reference
        public string Description { get; set; } = string.Empty; // Khởi tạo mặc định để tránh null reference
        public decimal Price { get; set; } = 0; // Giá trị mặc định là 0
        public string Genre { get; set; } = string.Empty; // Khởi tạo mặc định để tránh null reference
        public DateTime PublicationDate { get; set; } = DateTime.MinValue; // Giá trị mặc định là DateTime.MinValue
        public string ImageUrl { get; set; } = string.Empty; // Khởi tạo mặc định để tránh null reference
    }
}
