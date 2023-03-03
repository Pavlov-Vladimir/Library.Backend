using Library.Domain.Contracts.Repositories;
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
        return await _repository.UpdateBookAsync(book);
    }
}
