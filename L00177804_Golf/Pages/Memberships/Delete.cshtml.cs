

namespace L00177804_Golf.Pages.Memberships
{
    public class DeleteModel : PageModel
    {
        private readonly L00177804_GolfContext _context;

        public DeleteModel(L00177804_GolfContext context)
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

            var membership = await _context.Membership.FirstOrDefaultAsync(m => m.Id == id);

            if (membership == null)
            {
                return NotFound();
            }
            else 
            {
                Membership = membership;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Membership == null)
            {
                return NotFound();
            }
            var membership = await _context.Membership.FindAsync(id);

            if (membership != null)
            {
                Membership = membership;
                _context.Membership.Remove(Membership);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
