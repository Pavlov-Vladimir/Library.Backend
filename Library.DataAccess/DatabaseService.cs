using Library.Domain.Models;

namespace Library.DataAccess;
public class DatabaseService
{
    public static void Seed(ApplicationDbContext context)
    {
        Book[] books = new Book[10];
        for (int i = 0; i < books.Length; i++)
        {
            int index = i + 1;
            books[i] = new Book()
            {
                Author = "Author_" + index.ToString(),
                Title = "Title_" + index.ToString(),
                Content = "Lorem ipsum repeat hundreds times more...",
                Genre = i < 5 ? "horror" : "humor"
            };
        }
        context.Books.AddRange(books);
        context.SaveChanges();
    }
}
