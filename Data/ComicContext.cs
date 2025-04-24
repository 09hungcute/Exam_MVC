using Microsoft.EntityFrameworkCore;
using ComicRentalSystem.Models;

namespace ComicRentalSystem.Data
{
    public class ComicContext : DbContext
    {
        public ComicContext(DbContextOptions<ComicContext> options) : base(options) {}

        public DbSet<ComicBook> ComicBooks { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<RentalDetail> RentalDetails { get; set; }
    }
}
