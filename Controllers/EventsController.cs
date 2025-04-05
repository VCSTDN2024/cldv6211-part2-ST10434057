using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppPart1ST10434057.Data;
using WebAppPart1ST10434057.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppPart1ST10434057.Controllers
{
    public class EventsController : Controller
    {
        private readonly AppDbContext _context;

        public EventsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var events = await _context.Events.Include(e => e.Venue).ToListAsync();
            return View(events);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var ev = await _context.Events.Include(e => e.Venue).FirstOrDefaultAsync(m => m.EventID == id);
            if (ev == null) return NotFound();

            return View(ev);
        }

        public IActionResult Create()
        {
            ViewData["VenueID"] = new SelectList(_context.Venues, "VenueID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventID,Name,StartDate,EndDate,VenueID,ImageUrl")] Event ev)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ev);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VenueID"] = new SelectList(_context.Venues, "VenueID", "Name", ev.VenueID);
            return View(ev);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return NotFound();

            ViewData["VenueID"] = new SelectList(_context.Venues, "VenueID", "Name", ev.VenueID);
            return View(ev);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventID,Name,StartDate,EndDate,VenueID,ImageUrl")] Event ev)
        {
            if (id != ev.EventID) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(ev);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VenueID"] = new SelectList(_context.Venues, "VenueID", "Name", ev.VenueID);
            return View(ev);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var ev = await _context.Events.Include(e => e.Venue).FirstOrDefaultAsync(m => m.EventID == id);
            if (ev == null) return NotFound();

            return View(ev);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
// The EventsController handles all user interactions related to event management in the EventEase application. 
// It provides functionality for creating, viewing, editing, and deleting events. Each event is associated with a venue, 
// and the controller interacts with the database to perform CRUD operations on the Events and Venues tables. 
// It also includes logic for handling form submissions, validating inputs, and passing data to views for rendering.
// The controller ensures that all necessary data, such as venue information, is available when managing events.
