using Library.Domain.Common.Enums;
using Library.Domain.Models;

namespace Library.Domain.Contracts.Repositories;
public interface ILibraryRepository
{
    Task<int> CreateBookAsync(Book book, CancellationToken ct = default);
    Task<int> UpdateBookAsync(Book book, CancellationToken ct = default);
    Task DeleteBookAsync(int id, CancellationToken ct = default);
    Task<int> CreateReviewAsync(Review review, CancellationToken ct = default);
    Task AddRatingAsync(Rating rating, CancellationToken ct = default);
    Task<ICollection<Book>> GetAllBooksAsync(OrderByProperty orderBy, CancellationToken ct = default);
    Task<ICollection<Book>> GetRecommendedAsync(string? genre, CancellationToken ct = default);
    Task<Book?> GetBookByIdAsync(int bookId, CancellationToken ct = default);
}
