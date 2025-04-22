using AutoMapper;
using BookStore.Application.AuthorOperations.Queries.GetAuthors;
using BookStore.DbOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorByIdQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorByIdQueryTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void WhenAuthorIdDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {

            GetAuthorByIdQuery query = new GetAuthorByIdQuery(_context, _mapper);
            query.AuthorId = 999; 


            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("The author does not exist");
        }

        [Fact]
        public void WhenAuthorIdIsValid_Author_ShouldBeReturned()
        {

            var author = new Author { Name = "George", Surname = "Orwell", BirthDate = new DateTime(1903, 6, 25) };
            _context.Authors.Add(author);
            _context.SaveChanges();

            GetAuthorByIdQuery query = new GetAuthorByIdQuery(_context, _mapper);
            query.AuthorId = author.Id;

            var result = query.Handle();


            result.Should().NotBeNull();
            result.Name.Should().Be(author.Name);
            result.Surname.Should().Be(author.Surname);
        }
    }
}
