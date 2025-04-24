using Microsoft.AspNetCore.Mvc;
using ComicRentalSystem.Models;
using ComicRentalSystem.Data;
using System.Threading.Tasks;

public class CustomersController : Controller
{
    private readonly ComicContext _context;

    public CustomersController(ComicContext context)
    {
        _context = context;
    }

    // GET: Customers/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Customers/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Customer customer)
    {
        if (ModelState.IsValid)
        {
            try
            {
                customer.RegisterDate = DateTime.Now; // Set the register date
                _context.Add(customer);
                await _context.SaveChangesAsync();

                // Thêm thông báo thành công
                TempData["SuccessMessage"] = "Customer created successfully!";
                return RedirectToAction("Index", "ComicBooks");
            }
            catch (Exception ex)
            {
                // Nếu có lỗi trong quá trình lưu dữ liệu
                ModelState.AddModelError(string.Empty, "An error occurred while saving the customer. Please try again.");
                // Có thể log lỗi nếu cần
            }
        }

        return View(customer); // Trả về view nếu dữ liệu không hợp lệ hoặc có lỗi
    }
}
