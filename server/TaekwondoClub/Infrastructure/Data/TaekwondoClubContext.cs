using ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
            builder.Entity<Student>(ConfigureStudent);
            builder.Entity<Membership>(ConfigureMembership);
        }

        private void ConfigureStudent(EntityTypeBuilder<Student> builder)
        {
            builder
                .Property(p => p.Gender)
                .HasConversion<string>();
        }

        private void ConfigureMembership(EntityTypeBuilder<Membership> builder)
        {
            builder.Ignore(p => p.IsActive);
        }
    }
}
