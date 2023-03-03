using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using L00177804_Golf.Data;
using L00177804_Golf.Model;

namespace L00177804_Golf.Pages.MemberBookings
{
    public class DetailsModel : PageModel
    {
        private readonly L00177804_Golf.Data.L00177804_GolfContext _context;

        public DetailsModel(L00177804_Golf.Data.L00177804_GolfContext context)
        {
            _context = context;
        }

      public Booking Booking { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Booking == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }
            else 
            {
                Booking = booking;
            }
            return Page();
        }
    }
}
