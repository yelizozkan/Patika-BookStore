using AutoMapper;
using BookStore.Application.GenreOperations.Queries.GetGenres;
using BookStore.DbOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenreByIdQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreByIdQueryTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void WhenGenreIdDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {

            GetGenreByIdQuery query = new GetGenreByIdQuery(_context, _mapper);
            query.GenreId = 999;


            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>(); 
        }


        [Fact]
        public void WhenGenreIdIsValid_Genre_ShouldBeReturned()
        {

            var genre = new Genre { Name = "Query Test Genre" };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            GetGenreByIdQuery query = new GetGenreByIdQuery(_context, _mapper);
            query.GenreId = genre.Id;


            var result = query.Handle();

            result.Should().NotBeNull();
            result.Name.Should().Be(genre.Name);
        }
    }
}
