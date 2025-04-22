using AutoMapper;
using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using BookStore.DbOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommandTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistingAuthorNameIsGiven_InvalidOperationException_ShouldBeThrown()
        {

            var author = new Author { Name = "J.R.R.", Surname = "Tolkien", BirthDate = new DateTime(1892, 1, 3) };
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = new CreateAuthorCommand.CreateAuthorModel
            {
                Name = author.Name,
                Surname = author.Surname,
                BirthDate = author.BirthDate
            };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("The author already exists");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {

            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = new CreateAuthorCommand.CreateAuthorModel
            {
                Name = "J.K.",
                Surname = "Rowling",
                BirthDate = new DateTime(1965, 6, 25)
            };


            FluentActions.Invoking(() => command.Handle()).Invoke();

            var author = _context.Authors.SingleOrDefault(a => a.Name == command.Model.Name && a.Surname == command.Model.Surname);
            author.Should().NotBeNull();
        }
    }
}
