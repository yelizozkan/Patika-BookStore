using AutoMapper;
using BookStore.Application.AuthorOperations.Commands.DeleteAuthor;
using BookStore.DbOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteAuthorCommandTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void WhenAuthorIdDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {

            DeleteAuthorCommand command = new DeleteAuthorCommand(_context, _mapper);
            command.AuthorId = 999; 

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("Author not found!");
        }

        [Fact]
        public void WhenAuthorHasPublishedBooks_InvalidOperationException_ShouldBeThrown()
        {

            var author = new Author { Name = "George", Surname = "Orwell", BirthDate = new DateTime(1903, 6, 25) };
            var book = new Book { Title = "1984", GenreId = 1, PageCount = 300, PublishDate = DateTime.Now.AddYears(-5), Author = author };
            _context.Authors.Add(author);
            _context.Books.Add(book);
            _context.SaveChanges();

            DeleteAuthorCommand command = new DeleteAuthorCommand(_context, _mapper);
            command.AuthorId = author.Id;


            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("The author cannot be deleted because they have published books!");
        }

        [Fact]
        public void WhenAuthorIdIsValidAndHasNoBooks_Author_ShouldBeDeleted()
        {

            var author = new Author { Name = "Test", Surname = "Author", BirthDate = new DateTime(1980, 1, 1) };
            _context.Authors.Add(author);
            _context.SaveChanges();

            DeleteAuthorCommand command = new DeleteAuthorCommand(_context, _mapper);
            command.AuthorId = author.Id;


            FluentActions.Invoking(() => command.Handle()).Invoke();

            var deletedAuthor = _context.Authors.SingleOrDefault(a => a.Id == author.Id);
            deletedAuthor.Should().BeNull();
        }
    }
}
