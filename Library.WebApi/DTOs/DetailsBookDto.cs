namespace Library.WebApi.DTOs;

public class DetailsBookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string? Cover { get; set; }
    public string Genre { get; set; } = null!;
    public string Content { get; set; } = null!;
    public decimal Rating { get; set; }
    public List<DetailsReviewDto> Reviews { get; set; } = new List<DetailsReviewDto>();
}
