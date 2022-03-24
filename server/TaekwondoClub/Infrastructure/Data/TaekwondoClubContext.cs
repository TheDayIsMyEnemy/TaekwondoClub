using Microsoft.EntityFrameworkCore;
using ApplicationCore.Models;

namespace Infrastructure.Data
{
    public class TaekwondoClubContext : DbContext
    {
        public TaekwondoClubContext(DbContextOptions<TaekwondoClubContext> options) : base(options) { }

        public DbSet<Student> Students => Set<Student>();
        public DbSet<Group> Groups => Set<Group>();
        public DbSet<ClubMembership> ClubMemberships => Set<ClubMembership>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var dateColumnType = "date";

            builder
                .Entity<Student>()
                .Property(p => p.BirthDate)
                .HasColumnType(dateColumnType);
            builder
                .Entity<ClubMembership>()
                .Property(p => p.StartDate)
                .HasColumnType(dateColumnType);
            builder
                .Entity<ClubMembership>()
                .Property(p => p.EndDate)
                .HasColumnType(dateColumnType);
            builder
                .Entity<ClubMembership>()
                .Property(p => p.CreatedDate)
                .HasColumnType(dateColumnType);
            builder
                .Entity<ClubMembership>()
                .Ignore(p => p.IsActive);
        }
    }
}
