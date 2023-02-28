using Library.Domain.Models;

namespace Library.Domain.Contracts.Services;
public interface ILibraryService
{
    Task<int> AddBook(Book book);
    Task<int> UpdateBook(Book book);
    Task DeleteBook(int id);
    Task<int> AddReview(Review review);
    Task AddRating(Rating rating);
}
