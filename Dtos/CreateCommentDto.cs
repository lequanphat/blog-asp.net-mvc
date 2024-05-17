using System.ComponentModel.DataAnnotations;

public class CreateCommentDto
{
    [Required(ErrorMessage = "The message is required.")]
    public required string Message { get; set; }
}