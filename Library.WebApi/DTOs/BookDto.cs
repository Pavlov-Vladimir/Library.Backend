namespace Library.WebApi.DTOs;

[MessagePackObject]
public class BookDto
{
    [Key(0)]
    public int? Id { get; set; }
    [Key(1)]
    public string Title { get; set; } = null!;
    [Key(2)]
    public string? Cover { get; set; }
    [Key(3)]
    public string Content { get; set; } = null!;
    [Key(4)]
    public string Author { get; set; } = null!;
    [Key(5)]
    public string Genre { get; set; } = null!;
}
