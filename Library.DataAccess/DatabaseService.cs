using Library.Domain.Models;

namespace Library.DataAccess;
public class DatabaseService
{
    public static void Seed(ApplicationDbContext context)
    {
        Random random = new Random();
        Book[] books = new Book[21];
        for (int i = 0; i < books.Length; i++)
        {
            int index = i + 1;
            
            var book = new Book
            {
                Author = "Author_" + index,
                Title = "The Book " + index,
                Content = "Lorem ipsum repeat hundreds times more...",
                Genre = i < 5 ? "horror" : "humor",
            };

            var reviews = CreteReviews(random, book);
            book.Reviews = reviews;
            
            var ratings = CreateRatings(random, book);
            book.Ratings = ratings;

            books[i] = book;
        }
        
        context.Books.AddRange(books);
        
        context.SaveChanges();
    }

    private static Rating[] CreateRatings(Random random, Book book)
    {
        int randomIndex = random.Next(0, 20);
        Rating[] retings = new Rating[randomIndex];
        for (int j = 0; j < retings.Length; j++)
        {
            int randomScore = random.Next(1, 6);
            retings[j] = new Rating
            {
                Book = book,
                Score = randomScore
            };
        }

        return retings;
    }

    private static Review[] CreteReviews(Random random, Book book)
    {
        int randomIndex = random.Next(0, 10);
            
        Review[] reviews = new Review[randomIndex];
        for (int j = 0; j < reviews.Length; j++)
        {
            reviews[j] = new Review
            {
                Book = book,
                Message = "Review Lorem ipsum on the " + book.Title,
                Reviewer = "Reviewer_" + (j + 1),
            };
        }

        return reviews;
    }
}
