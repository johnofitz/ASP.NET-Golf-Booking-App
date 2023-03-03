using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using L00177804_Golf.Model;

namespace L00177804_Golf.Data
{
    public class L00177804_GolfContext : DbContext
    {
        public L00177804_GolfContext (DbContextOptions<L00177804_GolfContext> options)
            : base(options)
        {
        }


        public DbSet<L00177804_Golf.Model.Booking> Booking { get; set; } = default!;
        public DbSet<L00177804_Golf.Model.Membership> Membership { get; set; } = default!;
    }
}
