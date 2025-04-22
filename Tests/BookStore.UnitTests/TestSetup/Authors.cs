using BookStore.DbOperations;
using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
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
        }
    }
}
