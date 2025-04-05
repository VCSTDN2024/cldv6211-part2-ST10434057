using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppPart1ST10434057.Data;
using WebAppPart1ST10434057.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppPart1ST10434057.Controllers
{
    public class BookingsController : Controller
    {
        private readonly AppDbContext _context;

        public BookingsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var bookings = await _context.Bookings.Include(b => b.Event).ThenInclude(e => e.Venue).ToListAsync();
            return View(bookings);
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var booking = await _context.Bookings.Include(b => b.Event).ThenInclude(e => e.Venue).FirstOrDefaultAsync(m => m.BookingID == id);
            if (booking == null) return NotFound();

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["EventID"] = new SelectList(_context.Events, "EventID", "Name");
            return View();
        }

        // POST: Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingID,CustomerName,CustomerContact,BookingDate,EventID")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventID"] = new SelectList(_context.Events, "EventID", "Name", booking.EventID);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return NotFound();

            ViewData["EventID"] = new SelectList(_context.Events, "EventID", "Name", booking.EventID);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingID,CustomerName,CustomerContact,BookingDate,EventID")] Booking booking)
        {
            if (id != booking.BookingID) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventID"] = new SelectList(_context.Events, "EventID", "Name", booking.EventID);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var booking = await _context.Bookings.Include(b => b.Event).ThenInclude(e => e.Venue).FirstOrDefaultAsync(m => m.BookingID == id);
            if (booking == null) return NotFound();

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));



        }
    }
}// The BookingsController is responsible for managing the booking process within the EventEase application. 
// It allows users to perform CRUD (Create, Read, Update, Delete) operations on bookings, which are associated with specific events and venues. 
// The controller interacts with the AppDbContext to retrieve and manipulate booking data in the database. 
// It provides functionality to view a list of all bookings, view detailed information about a specific booking, 
// create new bookings, edit existing bookings, and delete bookings. Additionally, the controller ensures that 
// event information (like the event's venue) is included when displaying booking details. 
// The controller also validates user inputs and ensures that data is properly saved to the database after each operation.
