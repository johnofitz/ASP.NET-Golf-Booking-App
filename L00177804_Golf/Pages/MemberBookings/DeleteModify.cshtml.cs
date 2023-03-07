

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


        [BindProperty(SupportsGet = true)]
        public string CurrentFilter { get; set; } = default!;


        public async Task OnGetAsync()
        {
            if (_context.Booking != null)
            {
                Booking = await _context.Booking.ToListAsync();
            }
        }

        public async Task OnGetSearch()
        {

            var bookingQuery = _context.Booking.AsQueryable();
            try
            {
                CurrentFilter = CapitalizeFirstLetter(CurrentFilter);
                bookingQuery = bookingQuery.Where(s => s.Email == CurrentFilter);
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            Booking = await bookingQuery.ToListAsync();

        }

        private string CapitalizeFirstLetter(string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            return char.ToUpper(s[0]) + s[1..].ToLower();
        }
    }
}
