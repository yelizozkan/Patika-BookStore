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
                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Personal Growth"
                    },
                    new Genre
                    {
                        Name = "Science Fiction"
                    },
                    new Genre
                    {
                        Name = " Romance"
                    }
                );

                context.Books.AddRange(
                    new Book
                    {
                        Title = "The Great Gatsby",
                        GenreId = 1, 
                        PageCount = 180,
                        PublishDate = new DateTime(1925, 4, 10)
                    },
                    new Book
                    {
                        Title = "Herland",
                        GenreId = 2, 
                        PageCount = 250,
                        PublishDate = new DateTime(2010, 5, 10)
                    },
                    new Book
                    {
                        Title = "Dune",
                        GenreId = 2, 
                        PageCount = 540,
                        PublishDate = new DateTime(2001, 12, 21)
                    }
                );

                context.Authors.AddRange(
                    new Author
                    {
                        Name = "F. Scott Fitzgerald",
                        Surname = "Fitzgerald",
                        BirthDate = new DateTime(1896, 9, 24)
                    },
                    new Author
                    {
                        Name = "Charlotte Perkins",
                        Surname = "Gilman",
                        BirthDate = new DateTime(1860, 7, 3)
                    },
                    new Author
                    {
                        Name = "Frank Herbert",
                        Surname = "Herbert",
                        BirthDate = new DateTime(1920, 10, 8)
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
