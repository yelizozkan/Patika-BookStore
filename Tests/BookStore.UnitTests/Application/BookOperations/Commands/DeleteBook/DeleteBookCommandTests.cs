using BookStore.Application.BookOperations.Commands.DeleteBook;
using BookStore.DbOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteBookCommandTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public void WhenBookIdDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            // Arrange
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 999; // olmayan bir ID

            // Act & Assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>();
        }


        [Fact]
        public void WhenBookIdIsValid_Book_ShouldBeDeleted()
        {
            // Arrange
            var book = new Book { Title = "Test Book", PageCount = 100, PublishDate = DateTime.Now.Date.AddYears(-2), GenreId = 1 };
            _context.Books.Add(book);
            _context.SaveChanges();

            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = book.Id;

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var deletedBook = _context.Books.SingleOrDefault(x => x.Id == book.Id);
            deletedBook.Should().BeNull();
        }
    }
}
