namespace Library.Domain.Models;

public class Review
{
    public int Id { get; set; }
    public int BookIdId { get; set; }
    public string Message { get; set; } = null!;
    public string Reviewer { get; set; } = null!;
}