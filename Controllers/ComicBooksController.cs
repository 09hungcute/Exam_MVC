using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComicRentalSystem.Data;
using ComicRentalSystem.Models;

namespace ComicRentalSystem.Controllers
{
    public class ComicBooksController : Controller
    {
        private readonly ComicContext _context;

        public ComicBooksController(ComicContext context)
        {
            _context = context;
        }

        // GET: ComicBooks
        public async Task<IActionResult> Index()
        {
            var comics = await _context.ComicBooks.ToListAsync();
            return View(comics); // Trả về danh sách ComicBook
        }

        // GET: ComicBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comicBook = await _context.ComicBooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comicBook == null)
            {
                return NotFound();
            }

            return View(comicBook);
        }

        // GET: ComicBooks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ComicBooks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookName,Author,Price,PublicationDate,Genre,Description,ImageUrl")] ComicBook comicBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comicBook);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Comic book added successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(comicBook);
        }

        // GET: ComicBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comicBook = await _context.ComicBooks.FindAsync(id);
            if (comicBook == null)
            {
                return NotFound();
            }
            return View(comicBook);
        }

        // POST: ComicBooks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookName,Author,Price,PublicationDate,Genre,Description,ImageUrl")] ComicBook comicBook)
        {
            if (id != comicBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comicBook);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Comic book updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComicBookExists(comicBook.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(comicBook);
        }

        // POST: ComicBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comicBook = await _context.ComicBooks.FindAsync(id);
            if (comicBook == null)
            {
                return NotFound();
            }

            _context.ComicBooks.Remove(comicBook);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Comic book deleted successfully!";
            return RedirectToAction(nameof(Index)); // Quay lại trang danh sách
        }

        private bool ComicBookExists(int id)
        {
            return _context.ComicBooks.Any(e => e.Id == id);
        }
    }
}
