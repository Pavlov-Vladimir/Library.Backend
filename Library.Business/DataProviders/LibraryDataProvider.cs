using Library.Domain.Common.Enums;
using Library.Domain.Contracts.DataProviders;
using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Business.DataProviders;
public class LibraryDataProvider : ILibraryDataProvider
{
    public Task<Book> GetBookByIdAsync(int bookId)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Book>> GetBooksAsync(OrderByProperty? orderBy)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Book>> GetRecommendedAsync(string? genre)
    {
        throw new NotImplementedException();
    }
}
