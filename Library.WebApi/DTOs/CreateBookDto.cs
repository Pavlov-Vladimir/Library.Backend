namespace Library.WebApi.DTOs;

[MessagePackObject]
public class CreateBookDto
{
    [Key(0)]
    public string Title { get; set; } = null!;
    [Key(1)]
    public string? Cover { get; set; }
    [Key(2)]
    public string Content { get; set; } = null!;
    [Key(3)]
    public string Author { get; set; } = null!;
    [Key(4)]
    public string Genre { get; set; } = null!;
}
