using Library.Domain.Common.Enums;
using Library.Domain.Models;

namespace Library.Domain.Contracts.DataProviders;
public interface ILibraryDataProvider
{
    Task<ICollection<Book>> GetAll(OrderByProperty? orderBy);
    Task<ICollection<Book>> GetRecommended(string? genre);
    Task<Book> GetBook(int bookId);
}
