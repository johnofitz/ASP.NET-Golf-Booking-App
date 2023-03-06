

namespace L00177804_Golf.Pages.Memberships
{
    public class CreateModel : PageModel
    {
        private readonly Data.L00177804_GolfContext _context;

        public CreateModel(Data.L00177804_GolfContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Membership Membership { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Membership == null || Membership == null)
            {
                return Page();
            }

            // Return the list of names from object
            var checkEmails = EmailList();

            if (checkEmails.Contains(Membership.Email))
            {
                ModelState.AddModelError(string.Empty, "You already have an account with this email");
                return Page();
            }
            _context.Membership.Add(Membership);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        /// <summary>
        /// Check if Email already exists on Database
        /// </summary>
        /// <returns></returns>
        private List<string> EmailList()
        {
            List<string> emailList = new();

            foreach (var item in _context.Membership)
            {
                emailList.Add(item.Email);
            }
            return emailList;
        }
    }
}
