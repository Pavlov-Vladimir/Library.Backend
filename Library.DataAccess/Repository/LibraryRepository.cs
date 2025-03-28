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

    public async Task AddRatingAsync(Rating rating, CancellationToken ct = default)
    {
        if (rating == null)
            return;

        await _context.Ratings.AddAsync(rating, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<int> CreateBookAsync(Book book, CancellationToken ct = default)
    {
        if (book == null) throw new ArgumentNullException(nameof(book));

        await _context.Books.AddAsync(book, ct);
        await _context.SaveChangesAsync(ct);

        return book.Id;
    }

    public async Task<int> CreateReviewAsync(Review review, CancellationToken ct = default)
    {
        if (review == null) throw new ArgumentNullException(nameof(review));

        await _context.Reviews.AddAsync(review, ct);
        await _context.SaveChangesAsync(ct);

        return review.Id;
    }

    public async Task DeleteBookAsync(int id, CancellationToken ct = default)
    {
        var book = await _context.Books.FindAsync(id, ct);
        if (book != null)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync(ct);
        }
    }

    public async Task<ICollection<Book>> GetAllBooksAsync(OrderByProperty orderBy = OrderByProperty.Title, CancellationToken ct = default)
    {
        var books = _context.Books
            .AsNoTracking()
            .Include(b => b.Reviews)
            .Include(b => b.Ratings)
            .AsSplitQuery();

        return orderBy switch
        {
            OrderByProperty.Author => await books.OrderBy(b => b.Author).ToListAsync(ct),
            OrderByProperty.Title => await books.OrderBy(b => b.Title).ToListAsync(ct),
            _ => throw new ArgumentException("Invalid sort parameter.")
        };
    }

    public async Task<Book?> GetBookByIdAsync(int bookId, CancellationToken ct = default)
    {
        return await _context.Books
            .AsNoTracking()
            .Include(b => b.Reviews)
            .Include(b => b.Ratings)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == bookId, ct);
    }

    public async Task<ICollection<Book>> GetRecommendedAsync(string? genre = null, CancellationToken ct = default)
    {
        var query = _context.Books
            .AsNoTracking()
            .Include(b => b.Reviews)
            .Include(b => b.Ratings)
            .AsSplitQuery()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(genre))
        {
            query = query.Where(b => EF.Functions.ILike(b.Genre, $"%{genre}%"));
        }

        return await query
            .OrderByDescending(b => b.Ratings.Average(r => (int?)r.Score) ?? 0)
            .Take(10)
            .ToListAsync(ct);
    }

    public async Task<int> UpdateBookAsync(Book book, CancellationToken ct = default)
    {
        if (book == null) throw new ArgumentNullException(nameof(book));

        _context.Books.Update(book);
        await _context.SaveChangesAsync(ct);

        return book.Id;
    }

}
