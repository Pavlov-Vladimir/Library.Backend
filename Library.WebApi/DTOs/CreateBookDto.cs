namespace Library.WebApi.DTOs;

public class CreateBookDto
{
    public string Title { get; set; } = null!;
    public string? Cover { get; set; }
    public string Content { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string Genre { get; set; } = null!;
}
