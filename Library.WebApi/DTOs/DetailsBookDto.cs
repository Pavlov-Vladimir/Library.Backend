namespace Library.WebApi.DTOs;

[MessagePackObject]
public class DetailsBookDto
{
    [Key(0)]
    public int Id { get; set; }
    [Key(1)]
    public string Title { get; set; } = null!;
    [Key(2)]
    public string Author { get; set; } = null!;
    [Key(3)]
    public string? Cover { get; set; }
    [Key(4)]
    public string Genre { get; set; } = null!;
    [Key(5)]
    public string Content { get; set; } = null!;
    [Key(6)]
    public decimal Rating { get; set; }
    [Key(7)]
    public List<DetailsReviewDto> Reviews { get; set; } = new List<DetailsReviewDto>();
}
