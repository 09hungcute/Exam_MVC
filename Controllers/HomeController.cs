using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ComicRentalSystem.Models;
using ComicRentalSystem.Data;

namespace ComicRentalSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ComicContext _context;

        public HomeController(ILogger<HomeController> logger, ComicContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // Truy vấn thống kê tổng quan
            var totalBooks = _context.ComicBooks.Count();
            var totalCustomers = _context.Customers.Count();
            var totalRentals = _context.Rentals.Count();

            ViewBag.TotalBooks = totalBooks;
            ViewBag.TotalCustomers = totalCustomers;
            ViewBag.TotalRentals = totalRentals;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
