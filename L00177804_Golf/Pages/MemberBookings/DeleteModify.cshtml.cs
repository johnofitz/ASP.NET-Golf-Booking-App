

namespace L00177804_Golf.Pages.MemberBookings
{
    public class DeleteModifyModel : PageModel
    {
        private readonly L00177804_GolfContext _context;

        public DeleteModifyModel(L00177804_GolfContext context)
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
