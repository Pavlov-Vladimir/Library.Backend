using Library.Domain.Common.Enums;
using Library.Domain.Models;

namespace Library.Domain.Contracts.Repositories;
public interface ILibraryRepository
{
    Task<int> CreateBookAsync(Book book);
    Task<int> UpdateBookAsync(Book book);
    Task DeleteBookAsync(int id);
    Task<int> CreateReviewAsync(Review review);
    Task AddRatingAsync(Rating rating);
    Task<ICollection<Book>> GetAllBooksAsync(OrderByProperty orderBy);
    Task<ICollection<Book>> GetRecommendedAsync(string? genre);
    Task<Book?> GetBookByIdAsync(int bookId);
}
