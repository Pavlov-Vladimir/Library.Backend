using Library.Domain.Models;

namespace Library.Domain.Contracts.Services;
public interface ILibraryService
{
    Task<int> AddBookAsync(Book book);
    Task<int> UpdateBookAsync(Book book);
    Task DeleteBookAsync(int id);
    Task<int> AddReviewAsync(Review review);
    Task AddRatingAsync(Rating rating);
}
