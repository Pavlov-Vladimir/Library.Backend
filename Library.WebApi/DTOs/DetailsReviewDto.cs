namespace Library.WebApi.DTOs;

[MessagePackObject]
public class DetailsReviewDto
{
    [Key(0)]
    public int Id { get; set; }
    [Key(1)]
    public string Message { get; set; } = null!;
    [Key(2)]
    public string Reviewer { get; set; } = null!;

}
