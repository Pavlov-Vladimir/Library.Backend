namespace Library.Domain.Models;

public class Rating
{
    public int Id { get; set; }
    public Book Book { get; set; } = null!;
    public int Score { get; set; }
}