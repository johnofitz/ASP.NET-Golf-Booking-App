
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
