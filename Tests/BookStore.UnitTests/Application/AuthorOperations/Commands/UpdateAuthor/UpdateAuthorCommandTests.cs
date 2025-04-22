using BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using BookStore.DbOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateAuthorCommandTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public void WhenAuthorIdDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {

            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = 999; 
            command.Model = new UpdateAuthorCommand.UpdateAuthorModel
            {
                Name = "New Name",
                Surname = "New Surname",
                BirthDate = new DateTime(1980, 1, 1)
            };


            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("The author does not exist");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeUpdated()
        {

            var author = new Author { Name = "Old Name", Surname = "Old Surname", BirthDate = new DateTime(1950, 1, 1) };
            _context.Authors.Add(author);
            _context.SaveChanges();

            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = author.Id;
            command.Model = new UpdateAuthorCommand.UpdateAuthorModel
            {
                Name = "New Name",
                Surname = "New Surname",
                BirthDate = new DateTime(1980, 1, 1)
            };


            FluentActions.Invoking(() => command.Handle()).Invoke();

            var updatedAuthor = _context.Authors.SingleOrDefault(a => a.Id == author.Id);
            updatedAuthor.Should().NotBeNull();
            updatedAuthor.Name.Should().Be(command.Model.Name);
            updatedAuthor.Surname.Should().Be(command.Model.Surname);
            updatedAuthor.BirthDate.Should().Be(command.Model.BirthDate);
        }
    }
}
