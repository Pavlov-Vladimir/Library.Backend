using Library.Domain.Models;

namespace Library.DataAccess;
public class DatabaseService
{
    public static void Seed(ApplicationDbContext context)
    {
        if (context.Books.Any())
        {
            return;
        }
        
        Random random = new Random();
        Book[] books = new Book[21];
        for (int i = 0; i < books.Length; i++)
        {
            int index = i + 1;
            
            var book = new Book
            {
                Author = "Author_" + index,
                Title = "The Book " + index,
                Content = "Lorem ipsum dolor sit, amet consectetur adipisicing elit. Sunt at consequuntur laboriosam assumenda eum harum fugit nesciunt sequi deleniti deserunt, provident hic dolores. Expedita voluptates autem molestiae qui quia deleniti reprehenderit ullam dolor corrupti rerum nemo sunt quod asperiores saepe voluptate, unde velit, culpa rem! Incidunt dolorum reprehenderit, adipisci beatae quod nobis, veritatis distinctio voluptatum eligendi excepturi eius sint, omnis quibusdam a impedit sit ab. Magni aliquid non ea. Cum veniam quas quisquam explicabo unde quae, illum odio ullam, necessitatibus aperiam commodi a ipsam tempora nam. Eos veniam nesciunt deserunt labore laborum ullam cumque, aut ea cupiditate nemo aliquam tenetur?...",
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
        int randomIndex = random.Next(0, 6);
            
        Review[] reviews = new Review[randomIndex];
        for (int j = 0; j < reviews.Length; j++)
        {
            reviews[j] = new Review
            {
                Book = book,
                Message = "Review Lorem ipsum dolor sit, amet consectetur adipisicing elit. Sunt at consequuntur laboriosam assumenda eum harum fugit nesciunt sequi deleniti deserunt " + book.Title,
                Reviewer = "Reviewer_" + (j + 1),
            };
        }

        return reviews;
    }
}
