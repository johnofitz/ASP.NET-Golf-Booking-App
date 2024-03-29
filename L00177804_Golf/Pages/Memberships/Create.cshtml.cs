﻿

using System.Text.RegularExpressions;

namespace L00177804_Golf.Pages.Memberships
{
    public class CreateModel : PageModel
    {
        // Instantiate Object for database
        private readonly L00177804_GolfContext _context;

        public CreateModel(L00177804_GolfContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Membership Membership { get; set; } = default!;

        // Alert Mesege for Success/Unsuccess
        public string AlertMess { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        /// <summary>
        /// Post form data to Membership database
        /// </summary>
        /// <returns>Redirect to Members list</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            // Check if Valid and no null properties
            if (!ModelState.IsValid || _context.Membership == null || Membership == null)
            {
                AlertMess = "Oops, Something went wrong";
                return Page();
            }
            // Return the list of names from object
            var checkEmails = EmailList();

            // Check if email already exists in Members Table
            if (checkEmails.Contains(Membership.Email))
            {
                AlertMess = "Email Already exists";
                return Page();
            }

            if(!IsValidEmail(Membership.Email))
            {
                AlertMess = "Not a Valid Email";
                return Page();
            }

            if(!IsValidPhoneNumber(Membership.Phone))
            {
                AlertMess = "Phone Number requires 10-12 digits";
                return Page();
            }

            SetList();
            // Add new member
            _context.Membership.Add(Membership);
            await _context.SaveChangesAsync();

            // Return to database
            return RedirectToPage("./Index");
        }

        /// <summary>
        /// Check if Email already exists on Database
        /// </summary>
        /// <returns>A List of Emails from Member table</returns>
        private List<string> EmailList()
        {
            List<string> emailList = new();

            foreach (var item in _context.Membership)
            {
                emailList.Add(item.Email);
            }
            return emailList;
        }

        /// <summary>
        /// Method to Pepare items entered for adding to the database
        /// </summary>
        private void SetList()
        {
            if (_context.Membership != null)
            {
                Membership.Email = CapitalizeFirstLetter(Membership.Email);
                Membership.FirstName = CapitalizeFirstLetter(Membership.FirstName);
                Membership.LastName = CapitalizeFirstLetter(Membership.LastName);
            }
        }

        /// <summary>
        /// Method to format strings entered
        /// </summary>
        /// <param name="s"></param>
        /// <returns> strings formatted</returns>
        private static string CapitalizeFirstLetter(string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            return char.ToUpper(s[0]) + s[1..].ToLower();
        }


        public bool IsValidPhoneNumber(string phoneNumber)
        {
            Regex regex = new(@"^\+?\d{10,12}$"); // Regular expression for phone numbers
            return regex.IsMatch(phoneNumber);
        }

        public bool IsValidEmail(string email)
        {
            Regex regex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$"); // Regular expression for email addresses
            return regex.IsMatch(email);
        }
    }
}
