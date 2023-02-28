namespace Library.Domain.Models;
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Cover { get; set; }
    public string Content { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string Genre { get; set; } = null!;
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}
