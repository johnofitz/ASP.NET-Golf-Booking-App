

namespace L00177804_Golf.Pages.Memberships
{
    public class EditModel : PageModel
    {
        private readonly L00177804_GolfContext _context;

        public EditModel(L00177804_GolfContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Membership Membership { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Membership == null)
            {
                return NotFound();
            }

            var membership =  await _context.Membership.FirstOrDefaultAsync(m => m.Id == id);
            if (membership == null)
            {
                return NotFound();
            }
            Membership = membership;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Membership).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MembershipExists(Membership.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MembershipExists(int id)
        {
          return (_context.Membership?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
