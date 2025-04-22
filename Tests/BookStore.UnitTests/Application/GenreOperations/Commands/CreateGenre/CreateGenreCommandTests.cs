using BookStore.Application.GenreOperations.Commands.CreateGenre;
using BookStore.DbOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public CreateGenreCommandTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public void WhenAlreadyExistingGenreNameIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            var genre = new Genre { Name = "Adventure" };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = new CreateGenreCommand.CreateGenreModel { Name = genre.Name };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>(); 
        }


        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
        {

            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = new CreateGenreCommand.CreateGenreModel { Name = "Drama" };

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var genre = _context.Genres.SingleOrDefault(g => g.Name == command.Model.Name);
            genre.Should().NotBeNull();
        }
    }
}
