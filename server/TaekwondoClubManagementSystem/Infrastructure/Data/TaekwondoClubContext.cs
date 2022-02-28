using Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class TaekwondoClubContext : DbContext
    {
        public TaekwondoClubContext(DbContextOptions<TaekwondoClubContext> options) : base(options)
        {

        }

        public DbSet<Student> Students => Set<Student>();
        public DbSet<Group> Groups => Set<Group>();
        public DbSet<ClubMembership> ClubMemberships => Set<ClubMembership>();
    }
}
