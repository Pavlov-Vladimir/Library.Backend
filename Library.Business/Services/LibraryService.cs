using Library.Domain.Contracts.Repositories;
using Library.Domain.Contracts.Services;
using Library.Domain.Models;

namespace Library.Business.Services;
public class LibraryService : ILibraryService
{
    private readonly ILibraryRepository _repository;

    public LibraryService(ILibraryRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> AddBookAsync(Book book, CancellationToken ct = default)
    {
        return await _repository.CreateBookAsync(book, ct);
    }

    public async Task AddRatingAsync(Rating rating, CancellationToken ct = default)
    {
        await _repository.AddRatingAsync(rating, ct);
    }

    public async Task<int> AddReviewAsync(Review review, CancellationToken ct = default)
    {
        return await _repository.CreateReviewAsync(review, ct);
    }

    public async Task DeleteBookAsync(int id, CancellationToken ct = default)
    {
        await _repository.DeleteBookAsync(id, ct);
    }

    public async Task<int> UpdateBookAsync(Book book, CancellationToken ct = default)
    {
        var existing = await _repository.GetBookByIdAsync(book.Id, ct);
        if (existing == null)
            throw new InvalidOperationException("The book doesn't exist.");

        return await _repository.UpdateBookAsync(book, ct);
    }
}
