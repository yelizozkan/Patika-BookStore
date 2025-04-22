using BookStore.Application.GenreOperations.Commands.UpdateGenre;
using BookStore.DbOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateGenreCommandTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public void WhenGenreIdDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {

            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 999; 
            command.Model = new UpdateGenreCommand.UpdateGenreModel { Name = "New Name" };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("Genre not found!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeUpdated()
        {

            var genre = new Genre { Name = "Old Genre Name" };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = genre.Id;
            command.Model = new UpdateGenreCommand.UpdateGenreModel { Name = "New Genre Name" };


            FluentActions.Invoking(() => command.Handle()).Invoke();


            var updatedGenre = _context.Genres.SingleOrDefault(g => g.Id == genre.Id);
            updatedGenre.Should().NotBeNull();
            updatedGenre.Name.Should().Be(command.Model.Name);
        }
    }
}
