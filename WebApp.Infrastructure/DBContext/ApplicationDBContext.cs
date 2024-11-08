using Microsoft.EntityFrameworkCore;
using SpatialFocus.EntityFrameworkCore.Extensions;
using WebApp.Core.Domain.Entities;

namespace WebApp.Infrastructure.DBContext
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Gym> Gyms { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }
        public DbSet<Supplement> Supplements { get; set; }
        public DbSet<SupplementOrder> SupplementOrders { get; set; }
        public DbSet<MembershipPlan> MembershipPlans { get; set; }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureEnumLookup(
                EnumLookupOptions.Default
                .Singularize()
                .UseNumberAsIdentifier());

            modelBuilder.Entity<Member>()
            .HasIndex(m => m.MemberLoginName)
            .IsUnique();
        }
    }
}
