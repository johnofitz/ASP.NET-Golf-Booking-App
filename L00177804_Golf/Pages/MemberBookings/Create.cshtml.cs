﻿using System;
using System.Collections.Generic;
using System.Linq;

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
            // Empty database
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

            if (!ModelState.IsValid || _context.Booking == null || Booking == null)
            {
                ModelState.AddModelError(string.Empty, "There was a problem with your submission.");
                return Page();
            }

            if (!checkNames.Contains(Booking.FullName))
            {
                ModelState.AddModelError(string.Empty, "Check details or Register");
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


            // Add to booking table
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
