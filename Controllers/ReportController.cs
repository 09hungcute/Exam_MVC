using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComicRentalSystem.Data;


public class ReportController : Controller
{
    private readonly ComicContext _context;

    public ReportController(ComicContext context)
    {
        _context = context;
    }

    public IActionResult Index(DateTime? startDate, DateTime? endDate)
    {
        var query = _context.RentalDetails
            .Include(rd => rd.Rental)
            .ThenInclude(r => r.Customer)
            .Include(rd => rd.ComicBook)
            .AsQueryable();

        if (startDate.HasValue && endDate.HasValue)
        {
            query = query.Where(rd =>
                rd.Rental.RentalDate >= startDate && rd.Rental.RentalDate <= endDate);
        }

        var result = query.ToList();

        ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
        ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

        return View(result);
    }
}
