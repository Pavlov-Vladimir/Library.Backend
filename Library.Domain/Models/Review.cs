namespace Library.Domain.Models;

public class Review
{
    public int Id { get; set; }
    public Book Book { get; set; } = null!;
    public string Message { get; set; } = null!;
    public string Reviewer { get; set; } = null!;
}