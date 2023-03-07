

namespace L00177804_Golf.Pages.Memberships
{
    public class DetailsModel : PageModel
    {
        private readonly L00177804_GolfContext _context;

        public DetailsModel(L00177804_GolfContext context)
        {
            _context = context;
        }

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
    }
}
