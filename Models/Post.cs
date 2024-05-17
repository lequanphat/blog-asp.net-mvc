namespace Blog.Models
{
    public class Post
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required string Image { get; set; }
        public required Category Category { get; set; }
        public required User CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public Post()
        {
            CreatedAt = DateTime.Now;
        }
    }
}