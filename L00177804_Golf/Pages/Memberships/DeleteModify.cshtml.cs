

namespace L00177804_Golf.Pages.Memberships
{
    public class DeleteModifyModel : PageModel
    {
        private readonly L00177804_GolfContext _context;

        public DeleteModifyModel(L00177804_GolfContext context)
        {
            _context = context;
        }

        // string to bind to view for search
        [BindProperty(SupportsGet = true)]
        public string CurrentFilter { get; set; } = default!;

        // List of Membership objects
        public IList<Membership> Membership { get;set; } = default!;

        /// <summary>
        /// Method to excute on load
        /// </summary>
        /// <returns></returns>
        public async Task OnGetAsync()
        {
            if (_context.Membership != null)
            {
                Membership = await _context.Membership.ToListAsync();
            }
        }

        /// <summary>
        /// Search filter if email is correct 
        /// </summary>
        /// <returns>Retuns details of asscoiated with email</returns>
        public async Task OnGetSearch()
        {
            var memberQuery = _context.Membership.AsQueryable();
            try
            {
                CurrentFilter = CapitalizeFirstLetter(CurrentFilter);
                memberQuery = memberQuery.Where(s => s.Email == CurrentFilter);
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            Membership = await memberQuery.ToListAsync();

        }
        /// <summary>
        /// method to Capatilize first letter of string
        /// </summary>
        /// <param name="s"></param>
        /// <returns>Returns string value formatted</returns>
        private static string CapitalizeFirstLetter(string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            return char.ToUpper(s[0]) + s[1..].ToLower();
        }
    }
}
