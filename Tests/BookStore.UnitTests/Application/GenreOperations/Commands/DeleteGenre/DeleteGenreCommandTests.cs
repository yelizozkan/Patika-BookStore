using BookStore.Application.GenreOperations.Commands.DeleteGenre;
using BookStore.DbOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteGenreCommandTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public void WhenGenreIdDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {

            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 999; 


            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>(); 
        }


        [Fact]
        public void WhenGenreIdIsValid_Genre_ShouldBeDeleted()
        {
            // Arrange
            var genre = new Genre { Name = "Test Genre" };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = genre.Id;

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var deletedGenre = _context.Genres.SingleOrDefault(g => g.Id == genre.Id);
            deletedGenre.Should().BeNull();
        }
    }
}
