﻿using BookStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DbOperations
{
    public class BookStoreDbContext : DbContext,IBookStoreDbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }


      public override int SaveChanges ()
      {
            return base.SaveChanges();
      }

    }
}
