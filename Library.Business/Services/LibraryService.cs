using Library.Domain.Contracts.Services;
using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Business.Services;
public class LibraryService : ILibraryService
{
    public Task<int> AddBookAsync(Book book)
    {
        throw new NotImplementedException();
    }

    public Task AddRatingAsync(Rating rating)
    {
        throw new NotImplementedException();
    }

    public Task<int> AddReviewAsync(Review review)
    {
        throw new NotImplementedException();
    }

    public Task DeleteBookAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateBookAsync(Book book)
    {
        throw new NotImplementedException();
    }
}
