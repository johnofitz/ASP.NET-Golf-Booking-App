using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using L00177804_Golf.Data;
using L00177804_Golf.Model;

namespace L00177804_Golf.Pages.MemberBookings
{
    public class CreateModel : PageModel
    {
        private readonly L00177804_Golf.Data.L00177804_GolfContext _context;

        public CreateModel(L00177804_Golf.Data.L00177804_GolfContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()

        {
            //_context.Booking.ExecuteDeleteAsync();
            return Page();
        }

        [BindProperty]
        public Booking Booking { get; set; } = default!;


        public IList<Membership> Membership { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Booking == null || Booking == null)
            {
                ModelState.AddModelError(string.Empty, "There was a problem with your submission.");
                return Page();
            }

            foreach (var item in _context.Booking)
            {
                if (item.FirstName.Equals(Booking.FirstName) && item.BookingDate.Equals(Booking.BookingDate))
                {
                    ModelState.AddModelError(string.Empty, "You can only book once per day");

                    // Show the pop-up modal
                    TempData["Message"] = "You can only book once per day";
                    TempData["MessageType"] = "error";

                    return Page();
                }
            }



            // Match the Handicap in Database
            foreach (var item in _context.Membership)
            {
                if (item.FirstName.Equals(Booking.FirstName))
                {
                    Booking.Handicap = item.Handicap;
                    break;
                }
                
            }

            _context.Booking.Add(Booking);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
