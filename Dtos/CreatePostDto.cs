using System.ComponentModel.DataAnnotations;

public class CreatePostDto
{
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(100, MinimumLength = 20, ErrorMessage = "Title must be between 20 and 100 characters.")]
    public required string Title { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    [StringLength(500, MinimumLength = 20, ErrorMessage = "Description must be betwwen 20 and 00 characters.")]
    public required string Description { get; set; }
}