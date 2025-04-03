namespace Library.WebApi.DTOs;

[MessagePackObject]
public class RatingDto
{
    [Key(0)]
    public int Score { get; set; }
}
