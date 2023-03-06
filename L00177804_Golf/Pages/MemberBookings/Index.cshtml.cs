using L00177804_Golf.Data;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Intrinsics.X86;


namespace L00177804_Golf.Pages.MemberBookings
{
    public class IndexModel : PageModel
    {
        private readonly L00177804_GolfContext _context;

        public IndexModel(L00177804_GolfContext context)
        {
            _context = context;
        }

        public IList<Booking> Booking { get; set; } = default!;


        public IList<Membership> Membership { get; set; } = default!;


        [BindProperty(SupportsGet = true)]
        public string CurrentFilter { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Booking != null)
            {
                Booking = await _context.Booking.ToListAsync();
            }
        }

        public JsonResult OnGetNames()
        {
            List<string> nameList = new List<string>();

            foreach (var item in _context.Membership)
            {
                nameList.Add(item.FullName);
                Debug.WriteLine(item.FullName); // add a debug statement here
            }

            return new JsonResult(nameList);
        }



        public async Task OnGetSearch()
        {

            var bookingQuery = _context.Booking.AsQueryable();
            try
            {
                bookingQuery = bookingQuery.Where(s => s.FirstName+" "+s.LastName == CurrentFilter);
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            Booking = await bookingQuery.ToListAsync();

        }
    }
}
