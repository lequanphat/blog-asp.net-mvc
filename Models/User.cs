namespace Blog.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Avatar { get; set; }
        public Boolean IsAdmin { get; set; }
        public DateTime CreatedAt { get; set; }

        public User()
        {
            CreatedAt = DateTime.Now;
            IsAdmin = false;
        }
    }
}