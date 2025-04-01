using BookStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = serviceProvider.GetRequiredService<BookStoreDbContext>())
            {

                if (context.Books.Any())
                {
                    return;   
                }

                context.Books.AddRange(
                    new Book
                    {
                        Title = "The Great Gatsby",
                        GenreId = 1, //Personal Growth
                        PageCount = 180,
                        PublishDate = new DateTime(1925, 4, 10)
                    },
                    new Book
                    {
                        Title = "Herland",
                        GenreId = 2, //Science Fiction
                        PageCount = 250,
                        PublishDate = new DateTime(2010, 5, 10)
                    },
                    new Book
                    {
                        Title = "Dune",
                        GenreId = 2, //Science Fiction
                        PageCount = 540,
                        PublishDate = new DateTime(2001, 12, 21)
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
