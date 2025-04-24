using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComicRentalSystem.Data;
using ComicRentalSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class RentalsController : Controller
{
    private readonly ComicContext _context;

    public RentalsController(ComicContext context)
    {
        _context = context;
    }

    // GET: Rentals/Create
    public IActionResult Create()
    {
        ViewBag.Customers = new SelectList(_context.Customers, "Id", "FullName");
        ViewBag.ComicBooks = new SelectList(_context.ComicBooks, "Id", "BookName");
        return View();
    }

    // POST: Rentals/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        int customerId,
        DateTime rentalDate,
        List<int> comicBookIds,
        List<int> quantities,
        List<DateTime> returnDates,
        List<decimal> pricePerDay)
    {
        // Kiểm tra dữ liệu đầu vào
        if (comicBookIds == null || quantities == null || returnDates == null || pricePerDay == null)
        {
            ModelState.AddModelError("", "Thiếu thông tin thuê truyện.");
            return View();
        }

        // Tạo đối tượng Rental mới
        var rental = new Rental
        {
            CustomerId = customerId,
            RentalDate = rentalDate,
            RentalDetails = new List<RentalDetail>()
        };

        // Thêm các chi tiết thuê truyện vào RentalDetails
        for (int i = 0; i < comicBookIds.Count; i++)
        {
            rental.RentalDetails.Add(new RentalDetail
            {
                ComicBookId = comicBookIds[i],
                Quantity = quantities[i],
                ReturnDate = returnDates[i],
                PricePerDay = pricePerDay[i]
            });
        }

        // Thêm Rental vào cơ sở dữ liệu và lưu
        _context.Rentals.Add(rental);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index", "ComicBooks"); // Chuyển hướng tới trang ComicBooks sau khi lưu
    }

    // GET: Rentals/Index (Rental Report)
    public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
    {
        var rentalDetails = _context.RentalDetails
            .Include(rd => rd.ComicBook)
            .Include(rd => rd.Rental)
                .ThenInclude(r => r.Customer)
            .AsQueryable();

        // Lọc theo ngày thuê nếu có
        if (startDate.HasValue)
        {
            rentalDetails = rentalDetails.Where(rd => rd.Rental.RentalDate >= startDate.Value);
            ViewBag.StartDate = startDate.Value.ToString("yyyy-MM-dd");
        }

        if (endDate.HasValue)
        {
            rentalDetails = rentalDetails.Where(rd => rd.Rental.RentalDate <= endDate.Value);
            ViewBag.EndDate = endDate.Value.ToString("yyyy-MM-dd");
        }

        // Trả về danh sách RentalDetails để hiển thị trong view
        var result = await rentalDetails.ToListAsync();
        return View(result);
    }
}
