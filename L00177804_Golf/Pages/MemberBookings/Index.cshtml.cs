

using System.Globalization;

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

        // Alert Mesege for Success/Unsuccess
        public string AlertMess { get; set; } = default!;

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
            var nameList = _context.Booking.Select(item => item.FullName).ToList();
            return new JsonResult(nameList);
        }



        public async Task OnGetSearch()
        {

            var bookingQuery = _context.Booking.AsQueryable();
            try
            {
                if (!string.IsNullOrEmpty(CurrentFilter))
                {
                    CapString();
                    bookingQuery = bookingQuery.Where(s => s.FirstName + " " + s.LastName == CurrentFilter);
                    Booking = await bookingQuery.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            finally
            {
                // Return the list of names from object
                var checkNames = BookingNameList();

                // Check if email already exists in Members Table
                if (!checkNames.Contains(CurrentFilter))
                {
                    Booking = await _context.Booking.ToListAsync();
                    AlertMess = "You dont have a booking";
                }
            }
          

        }

        /// <summary>
        /// Method to Pepare items entered for adding to the database
        /// </summary>
        private void CapString()
        {
            if (!string.IsNullOrEmpty(CurrentFilter))
            {
                TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
                CurrentFilter = textInfo.ToTitleCase(CurrentFilter);

            }
        }

        /// <summary>
        /// Check if Email already exists on Database
        /// </summary>
        /// <returns>A List of Emails from Member table</returns>
        private List<string> BookingNameList()
        {
            List<string> nameList = new();

            foreach (var item in _context.Booking)
            {
                nameList.Add(item.FullName);
            }
            return nameList;
        }
    }
}
