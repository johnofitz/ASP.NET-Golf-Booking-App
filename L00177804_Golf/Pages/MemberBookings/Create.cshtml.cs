using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using L00177804_Golf.Data;
using L00177804_Golf.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace L00177804_Golf.Pages.MemberBookings
{
    public class CreateModel : PageModel
    {
        private readonly L00177804_Golf.Data.L00177804_GolfContext _context;
        private bool _check = true; 
        public CreateModel(L00177804_Golf.Data.L00177804_GolfContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()

        {
            //_context.Booking.ExecuteDeleteAsync();
            //_context.Membership.ExecuteDeleteAsync();
            return Page();
        }

        [BindProperty]
        public Booking Booking { get; set; } = default!;


        public IList<Membership> Membership { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            // Return the list of names from object
            var checkNames = NameList();

            if (!checkNames.Contains(Booking.FullName))
            {
                ModelState.AddModelError(string.Empty, "Check details or Register");
                return Page();
            }

            //if (!_context.Membership.Any(m => m.FullName == Booking.FullName))
            //{
            //    ModelState.AddModelError(string.Empty, "You can only book once per day");
            //    return Page();
            //    // execute code if a Membership object with the given FullName exists in the Membership list
            //}

            if (!ModelState.IsValid || _context.Booking == null || Booking == null)
            {
                ModelState.AddModelError(string.Empty, "There was a problem with your submission.");
                return Page();
            }

            // Check if person has booked the same day
            foreach (var item in _context.Booking)
            {
                if (item.FullName.Equals(Booking.FullName) && item.BookingDate.Equals(Booking.BookingDate))
                {
                    ModelState.AddModelError(string.Empty, "You can only book once per day");
                    return Page();
                }


            }

            //Match the Handicap in Database
            foreach (var item in _context.Membership)
            {
                if (item.FullName.Equals(Booking.FullName))
                {
                    Booking.Handicap = item.Handicap;
                    break;
                }
            }



            _context.Booking.Add(Booking);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        
        /// <summary>
        /// Method for checking name validation
        /// </summary>
        /// <returns>A list of full names from database </returns>
        private List<string> NameList()
        {
            List<string> nameList = new();

            foreach (var item in _context.Membership)
            {
                nameList.Add(item.FullName);
            }
            return nameList;
        }


    }
}
