namespace Data.Models
{
    public class Group
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
