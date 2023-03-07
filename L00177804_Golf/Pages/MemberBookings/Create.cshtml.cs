
namespace L00177804_Golf.Pages.MemberBookings
{
    public class CreateModel : PageModel
    {
        // Instantiate object for Data base
        private readonly L00177804_GolfContext _context;
        
        public CreateModel(L00177804_GolfContext context)
        {
            _context = context;
        }

        /// <summary>
        /// OnGet Method excutes on page load
        /// </summary>
        /// <returns></returns>
        public IActionResult OnGet()

        {
            // Empty database
           // _context.Booking.ExecuteDeleteAsync();
           // _context.Membership.ExecuteDeleteAsync();
            return Page();
        }

        // Booking Object for Razor View
        [BindProperty]
        public Booking Booking { get; set; } = default!;

        // Alert Mesege for Success/Unsuccess
        public string AlertMess { get; set; } = default!;

        // List for Member Cross reference
        public IList<Membership> Membership { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            
            if (_context.Booking == null || Booking == null)
            {
                AlertMess = "Oops, Something went wrong";
                return Page();
            }
            // Return the list of names from object
            var checkNames = NameList();
            // Display modal if Name dosnt exist
            if (!checkNames.Contains(Booking.FullName))
            {
                AlertMess = "You dont seem to have an account, Please register";
                return Page();
            }

            //Match the Handicap in Database
            foreach (var item in _context.Membership)
            {
                if (item.FullName.Equals(Booking.FullName))
                {
                    Booking.Handicap = item.Handicap;
                    Booking.Email = item.Email;
                    break;
                }
            }
            SetList();

            // Check if person has booked the same day
            foreach (var item in _context.Booking)
            {
                if (item.FullName.Equals(Booking.FullName) && item.BookingDate.Equals(Booking.BookingDate))
                {
                    AlertMess = "You can only book once per day";
                    return Page();
                }
            }

        
            // Check count for booking on same time slot of specfic day
            if (CheckCount(Booking.BookingDate, Booking.BookingTime) == 4)
            {
                AlertMess = "All slots have been taken for this time";
                return Page();
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

        /// <summary>
        /// Method to check the amount of bookings at a given time
        /// slot on a specfic date
        /// </summary>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        private int CheckCount(string date, string time)
        {
            int count = 0;
            foreach (var item in _context.Booking)
            {
                if(item.BookingDate == date &&  item.BookingTime == time)
                {
                    count++;
                }
            }
            return count;
        }


         private void SetList()
        {
            if (_context.Booking != null)
            {   
                Booking.FirstName = CapitalizeFirstLetter(Booking.FirstName);
                Booking.LastName = CapitalizeFirstLetter(Booking.LastName);
            }
        }

        private static string CapitalizeFirstLetter(string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            return char.ToUpper(s[0]) + s[1..].ToLower();
        }

    }
}
