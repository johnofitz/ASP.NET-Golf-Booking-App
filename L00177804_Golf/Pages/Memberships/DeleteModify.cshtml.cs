using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using L00177804_Golf.Data;
using L00177804_Golf.Model;

namespace L00177804_Golf.Pages.Memberships
{
    public class DeleteModifyModel : PageModel
    {
        private readonly L00177804_Golf.Data.L00177804_GolfContext _context;

        public DeleteModifyModel(L00177804_Golf.Data.L00177804_GolfContext context)
        {
            _context = context;
        }

        public IList<Membership> Membership { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Membership != null)
            {
                Membership = await _context.Membership.ToListAsync();
            }
        }
    }
}
