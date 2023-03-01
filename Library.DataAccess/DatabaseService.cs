using Library.Domain.Models;

namespace Library.DataAccess;
public class DatabaseService
{
    public static void Seed(ApplicationDbContext context)
    {
        Book[] books = new Book[10];
        for (int i = 0; i < books.Length; i++)
        {
            books[i] = new Book()
            {
                Id = i,
                Author = "Author_" + i.ToString(),
                Title = "Title_" + i.ToString(),
                Content = "Lorem ipsum repeat hundreds times more...",
                Genre = i < 5 ? "horror" : "humor"
            };
        }
        context.Books.AddRange(books);
        context.SaveChanges();
    }
}
