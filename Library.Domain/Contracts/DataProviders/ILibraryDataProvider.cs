using Library.Domain.Common.Enums;
using Library.Domain.Models;

namespace Library.Domain.Contracts.DataProviders;
public interface ILibraryDataProvider
{
    Task<ICollection<Book>> GetBooksAsync(OrderByProperty orderBy = OrderByProperty.Title);
    Task<ICollection<Book>> GetRecommendedAsync(string? genre);
    Task<Book?> GetBookByIdAsync(int bookId);
}
