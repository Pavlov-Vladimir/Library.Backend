using Library.Domain.Common.Enums;
using Library.Domain.Models;

namespace Library.Domain.Contracts.DataProviders;
public interface ILibraryDataProvider
{
    Task<ICollection<Book>> GetBooksAsync(OrderByProperty orderBy = OrderByProperty.Title, CancellationToken ct = default);
    Task<ICollection<Book>> GetRecommendedAsync(string? genre, CancellationToken ct = default);
    Task<Book?> GetBookByIdAsync(int bookId, CancellationToken ct = default);
}
