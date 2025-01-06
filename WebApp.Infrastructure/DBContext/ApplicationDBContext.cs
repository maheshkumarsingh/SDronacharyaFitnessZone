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
        public DbSet<Photo> Photos { get; set; }
        public DbSet<MemberMentor> MemberMentors { get; set; }
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

            modelBuilder.Entity<MemberMentor>()
                    .HasKey(k => new { k.MentorLoginName, k.MemberLoginName });

            modelBuilder.Entity<MemberMentor>()
                    .HasOne(s => s.Mentor)
                    .WithMany(t => t.Mentoring)
                    .HasForeignKey(s => s.MentorLoginName)
                    .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<MemberMentor>()
                    .HasOne(s => s.Member)
                    .WithMany(t => t.MentoredBy)
                    .HasForeignKey(s => s.MemberLoginName)
                    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
