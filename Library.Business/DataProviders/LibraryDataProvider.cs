using Library.Domain.Common.Enums;
using Library.Domain.Contracts.DataProviders;
using Library.Domain.Contracts.Repositories;
using Library.Domain.Models;

namespace Library.Business.DataProviders;
public class LibraryDataProvider : ILibraryDataProvider
{
    private readonly ILibraryRepository _repository;

    public LibraryDataProvider(ILibraryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Book?> GetBookByIdAsync(int bookId)
    {
        return await _repository.GetBookByIdAsync(bookId);
    }

    public async Task<ICollection<Book>> GetBooksAsync(OrderByProperty orderBy)
    {
        return await _repository.GetAllBooksAsync(orderBy);
    }

    public async Task<ICollection<Book>> GetRecommendedAsync(string? genre)
    {
        return await _repository.GetRecommendedAsync(genre);
    }
}
