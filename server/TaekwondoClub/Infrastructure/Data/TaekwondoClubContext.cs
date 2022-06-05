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

        public DbSet<MembershipHistory> MembershipHistories => Set<MembershipHistory>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Student>(ConfigureStudent);
            builder.Entity<Membership>(ConfigureMembership);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        //entry.Entity.CreatedBy = TO DO
                        entry.Entity.Created = DateTime.Now;
                        //entry.Entity.LastModifiedBy = To DO
                        entry.Entity.LastModified = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        //entry.Entity.LastModifiedBy = To DO
                        entry.Entity.LastModified = DateTime.Now;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        private void ConfigureStudent(EntityTypeBuilder<Student> builder)
        {
            builder
                .Property(p => p.Gender)
                .HasConversion<string>();
            builder
                .Property(p => p.BirthDate)
                .HasColumnType("date");
        }

        private void ConfigureMembership(EntityTypeBuilder<Membership> builder)
        {
            builder.Ignore(p => p.IsActive);
        }
    }
}
