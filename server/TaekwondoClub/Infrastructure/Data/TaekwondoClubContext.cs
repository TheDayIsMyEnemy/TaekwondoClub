using Microsoft.EntityFrameworkCore;
using ApplicationCore.Models;

namespace Infrastructure.Data
{
    public class TaekwondoClubContext : DbContext
    {
        public TaekwondoClubContext(DbContextOptions<TaekwondoClubContext> options) : base(options) { }

        public DbSet<Student> Students => Set<Student>();
        public DbSet<Group> Groups => Set<Group>();
        public DbSet<Membership> Memberships => Set<Membership>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var dateColumnType = "date";

            builder
                .Entity<Student>()
                .Property(p => p.BirthDate)
                .HasColumnType(dateColumnType);
            builder
                .Entity<Membership>()
                .Property(p => p.StartDate)
                .HasColumnType(dateColumnType);
            builder
                .Entity<Membership>()
                .Property(p => p.EndDate)
                .HasColumnType(dateColumnType);
            builder
                .Entity<Membership>()
                .Property(p => p.CreatedDate)
                .HasColumnType(dateColumnType);
            builder
                .Entity<Membership>()
                .Ignore(p => p.IsActive);
        }
    }
}
