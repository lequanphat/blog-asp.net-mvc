namespace Blog.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public required string Message { get; set; }
        public required Post Post { get; set; }
        public required User CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        
        public Comment()
        {
            CreatedAt = DateTime.Now;
        }
    }
}