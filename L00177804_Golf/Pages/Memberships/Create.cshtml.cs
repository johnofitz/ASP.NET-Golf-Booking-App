

namespace L00177804_Golf.Pages.Memberships
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

            _context.Membership.Add(Membership);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
