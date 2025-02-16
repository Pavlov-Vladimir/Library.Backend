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

    public async Task<int> AddBookAsync(Book book)
    {
        return await _repository.CreateBookAsync(book);
    }

    public async Task AddRatingAsync(Rating rating)
    {
        await _repository.AddRatingAsync(rating);
    }

    public async Task<int> AddReviewAsync(Review review)
    {
        return await _repository.CreateReviewAsync(review);
    }

    public async Task DeleteBookAsync(int id)
    {
        await _repository.DeleteBookAsync(id);
    }

    public async Task<int> UpdateBookAsync(Book book)
    {
        var existing = await _repository.GetBookByIdAsync(book.Id);
        if (existing == null)
            throw new InvalidOperationException("The book doesn't exist.");

        return await _repository.UpdateBookAsync(book);
    }
}
