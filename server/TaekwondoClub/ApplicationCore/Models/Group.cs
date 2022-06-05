namespace ApplicationCore.Models
{
    public class Group : AuditableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public ICollection<Student> Students { get; set; } = null!;
    }
}
