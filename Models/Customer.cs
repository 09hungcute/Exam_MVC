namespace ComicRentalSystem.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty; // Khởi tạo mặc định để tránh null reference
        public string PhoneNumber { get; set; } = string.Empty; // Khởi tạo mặc định để tránh null reference
        public DateTime RegisterDate { get; set; } // Kiểu DateTime không thể null, không cần khởi tạo
    }
}
