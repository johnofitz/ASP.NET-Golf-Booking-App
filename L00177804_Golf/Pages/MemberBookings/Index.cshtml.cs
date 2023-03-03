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
    public class IndexModel : PageModel
    {
        private readonly L00177804_Golf.Data.L00177804_GolfContext _context;

        public IndexModel(L00177804_Golf.Data.L00177804_GolfContext context)
        {
            _context = context;
        }

        public IList<Booking> Booking { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Booking != null)
            {
                Booking = await _context.Booking.ToListAsync();
            }
        }
    }
}
