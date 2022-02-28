using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Data.Models
{
    public class ClubMembership
    {
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }

        public string? Note { get; set; }

        public Student Student { get; set; } = null!;

        [NotMapped]
        public bool IsActive => DateTime.Now.Date < EndDate.Date;
    }
}
