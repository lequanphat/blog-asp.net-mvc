namespace Blog.Models
{
    public class Category{
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public Category()
        {
            CreatedAt = DateTime.Now;
        }
    }
}