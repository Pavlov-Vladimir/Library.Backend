namespace Library.WebApi.DTOs;

public class GetBookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public decimal Rating { get; set; }
    public decimal ReviewsNumber { get; set; }
}
