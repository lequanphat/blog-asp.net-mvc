using System.ComponentModel.DataAnnotations;

public class CreatePostDto
{
    [Required(ErrorMessage = "The title is required.")]
    [StringLength(100, MinimumLength = 10, ErrorMessage = "The title must be between 10 and 100 characters.")]
    public required string Title { get; set; }

    [Required(ErrorMessage = "The description is required.")]
    [StringLength(500, MinimumLength = 20, ErrorMessage = "The description must be betwwen 20 and 00 characters.")]
    public required string Description { get; set; }

    [Required(ErrorMessage = "The image is required.")]
    public required IFormFile Image { get; set; }

    [Required(ErrorMessage = "The category is required.")]
    public required int CategoryId { get; set; }
}