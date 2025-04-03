namespace Library.WebApi.DTOs;

[MessagePackObject]
public class GetBookDto
{
    [Key(0)]
    public int Id { get; set; }
    [Key(1)]
    public string Title { get; set; } = null!;
    [Key(2)]
    public string Author { get; set; } = null!;
    [Key(3)]
    public decimal Rating { get; set; }
    [Key(4)]
    public decimal ReviewsNumber { get; set; }
}
