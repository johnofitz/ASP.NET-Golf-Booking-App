

using Microsoft.EntityFrameworkCore;

namespace L00177804_Golf.Pages.MemberBookings
{
    public class EditModel : PageModel
    {
        private readonly L00177804_GolfContext _context;

        public EditModel(L00177804_GolfContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Booking Booking { get; set; } = default!;

        //[BindProperty]
        //public Booking BookingNew { get; set; } = default!;

        // Alert Mesege for Success/Unsuccess
        public string AlertMess { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Booking == null)
            {
                return NotFound();
            }

            var booking =  await _context.Booking.FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }
            Booking = booking;
            


 
            return Page();
            // Now you can add or attach a new instance of the Booking entity with the same key value

        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var existingBooking = _context.Booking.Find(Booking.Id); // Find the existing entity with the specified key value

            if (!ModelState.IsValid)
            {
                return Page();
            }
            //// Check count for booking on same time slot of specfic day
            if (CheckCount(Booking.BookingDate, Booking.BookingTime) == 4)
            {
                AlertMess = "All slots have been taken for this time";
                return Page();
            }


        
            if (existingBooking != null)
            {
                var entry = _context.Entry(existingBooking); // Get the EntityEntry for the existing entity
                if (entry.State != EntityState.Detached) // Check if the entity is already detached
                {
                    entry.State = EntityState.Detached; // Detach the entity from the change tracker
                }
            }

            if (!_context.ChangeTracker.Entries<Booking>().Any(e => e.Entity.Id == Booking.Id))
            {

                // Save changes to the database
                await _context.SaveChangesAsync();
                _context.Attach(Booking).State = EntityState.Modified;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(Booking.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BookingExists(int id)
        {
          return (_context.Booking?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        /// <summary>
        /// Method to check the amount of bookings at a given time
        /// slot on a specfic date
        /// </summary>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        private int CheckCount(string date, string time)
        {
            int count = 0;
            foreach (var item in _context.Booking)
            {
                if (item.BookingDate == date && item.BookingTime == time)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
