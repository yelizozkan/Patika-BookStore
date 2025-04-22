using BookStore.Application.BookOperations.Commands.UpdateBook;
using BookStore.DbOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateBookCommandTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public void WhenBookIdDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = 999; 
            command.Model = new UpdateBookCommand.UpdateBookModel { Title = "Updated Title" };

            
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>();
        }


        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
        {
            
            var book = new Book { Title = "Old Title", PageCount = 100, PublishDate = DateTime.Now.Date.AddYears(-5), GenreId = 1 };
            _context.Books.Add(book);
            _context.SaveChanges();

            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = book.Id;
            command.Model = new UpdateBookCommand.UpdateBookModel
            {
                Title = "New Title",
                PageCount = 250,
                GenreId = 2
            };

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var updatedBook = _context.Books.SingleOrDefault(b => b.Id == book.Id);
            updatedBook.Should().NotBeNull();
            updatedBook.Title.Should().Be(command.Model.Title);
            updatedBook.PageCount.Should().Be(command.Model.PageCount);
            updatedBook.GenreId.Should().Be(command.Model.GenreId);
        }
    }
}
