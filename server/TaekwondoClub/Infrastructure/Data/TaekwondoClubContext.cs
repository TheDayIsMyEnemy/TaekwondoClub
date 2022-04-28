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

        private const string OnlyDateColumnType = "date";

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Student>(ConfigureStudent);
            builder.Entity<Membership>(ConfigureMembership);
        }

        private void ConfigureStudent(EntityTypeBuilder<Student> builder)
        {
            builder
                .Property(p => p.BirthDate)
                .HasColumnType(OnlyDateColumnType);
            builder
                .Property(p => p.Gender)
                .HasConversion<string>();
        }

        private void ConfigureMembership(EntityTypeBuilder<Membership> builder)
        {
            builder
                .Property(p => p.StartDate)
                .HasColumnType(OnlyDateColumnType);
            builder
                .Property(p => p.EndDate)
                .HasColumnType(OnlyDateColumnType);
            builder
                .Property(p => p.CreatedDate)
                .HasColumnType(OnlyDateColumnType);
            builder
                .Ignore(p => p.IsActive);
        }
    }
}
