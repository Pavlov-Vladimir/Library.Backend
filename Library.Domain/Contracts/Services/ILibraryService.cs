using Library.Domain.Models;

namespace Library.Domain.Contracts.Services;
public interface ILibraryService
{
    Task<int> AddBookAsync(Book book, CancellationToken ct = default);
    Task<int> UpdateBookAsync(Book book, CancellationToken ct = default);
    Task DeleteBookAsync(int id, CancellationToken ct = default);
    Task<int> AddReviewAsync(Review review, CancellationToken ct = default);
    Task AddRatingAsync(Rating rating, CancellationToken ct = default);
}
