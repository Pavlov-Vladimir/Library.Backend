using Library.Domain.Common.Enums;
using Library.Domain.Contracts.Repositories;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess.Repository;
public class LibraryRepository : ILibraryRepository
{
    private readonly ApplicationDbContext _context;

    public LibraryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddRatingAsync(Rating rating)
    {
        if (rating == null)
            return;

        await _context.Ratings.AddAsync(rating);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CreateBookAsync(Book book)
    {
        if (book == null)
            return await Task.FromResult(-1);

        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();

        return await Task.FromResult(book.Id);
    }

    public async Task<int> CreateReviewAsync(Review review)
    {
        if (review == null)
            return await Task.FromResult(-1);

        await _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();

        return await Task.FromResult(review.Id);
    }

    public async Task DeleteBookAsync(int id)
    {
        var book = await GetBookByIdAsync(id);
        if (book != null)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<ICollection<Book>> GetAllBooksAsync(OrderByProperty orderBy = OrderByProperty.Title)
    {
        return orderBy switch
        {
            OrderByProperty.Author => await _context.Books.OrderBy(b => b.Author).ToListAsync(),
            OrderByProperty.Title => await _context.Books.OrderBy(b => b.Title).ToListAsync(),
            _ => throw new ArgumentException("Invalid sort parameter.")
        };
    }

    public async Task<Book?> GetBookByIdAsync(int bookId)
    {
        return await _context.Books
            .Include(b => b.Reviews)
            .Include(b => b.Ratings)
            .SingleOrDefaultAsync(x => x.Id == bookId);
    }

    public async Task<ICollection<Book>> GetRecommendedAsync(string? genre = null)
    {
        if (genre == null)
            return await _context.Books
                .Include(b => b.Ratings)
                .OrderBy(b => b.Ratings.Average(r => r.Score))
                .Take(10)
                .ToListAsync();

        return await _context.Books
            .Where(b => b.Genre.ToLower().Contains(genre.ToLower()))
            .Include(b => b.Ratings)
            .OrderBy(b => b.Ratings.Average(r => r.Score))
            .Take(10)
            .ToListAsync();
    }

    public async Task<int> UpdateBookAsync(Book book)
    {
        var existing = await GetBookByIdAsync(book.Id);
        if (existing == null)
            throw new InvalidOperationException("The book doesn't exist.");

        existing.Title = book.Title;
        existing.Author = book.Author;
        existing.Genre = book.Genre;
        existing.Content = book.Content;
        existing.Cover = book.Cover;
        await _context.SaveChangesAsync();

        return existing.Id;
    }
}
