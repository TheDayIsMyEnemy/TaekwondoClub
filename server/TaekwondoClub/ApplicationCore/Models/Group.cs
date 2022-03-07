namespace ApplicationCore.Models
{
    public class Group
    {
        public Group(string name)
        {
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Student> Students { get; set; } = null!;
    }
}
