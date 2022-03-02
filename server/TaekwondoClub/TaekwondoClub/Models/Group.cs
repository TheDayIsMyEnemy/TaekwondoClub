namespace TaekwondoClub.Models
{
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Student> Students { get; set; } = null!;

        public Group(string name)
        {
            Name = name;
        }
    }
}
