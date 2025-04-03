namespace Library.WebApi.DTOs;

[MessagePackObject]
public class ReviewDto
{
    [Key(0)]
    public string Message { get; set; } = null!;
    [Key(1)]
    public string Reviewer { get; set; } = null!;
}
