using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using L00177804_Golf.Data;
using L00177804_Golf.Model;

namespace L00177804_Golf.Pages.MemberBookings
{
    public class EditModel : PageModel
    {
        private readonly L00177804_Golf.Data.L00177804_GolfContext _context;

        public EditModel(L00177804_Golf.Data.L00177804_GolfContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Booking Booking { get; set; } = default!;

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
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            // Check count for booking on same time slot of specfic day
            if (CheckCount(Booking.BookingDate, Booking.BookingTime) == 4)
            {
                AlertMess = "All slots have been taken for this time";
                return Page();
            }

            _context.Attach(Booking).State = EntityState.Modified;

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
