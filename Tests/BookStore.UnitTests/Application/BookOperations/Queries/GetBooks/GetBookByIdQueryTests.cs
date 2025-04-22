using AutoMapper;
using BookStore.Application.BookOperations.Queries.GetBooks;
using BookStore.DbOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Application.BookOperations.Queries.GetBooks
{
    public class GetBookByIdQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookByIdQueryTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void WhenBookIdDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {

            GetBookByIdQuery query = new GetBookByIdQuery(_context, _mapper);
            query.BookId = 999; 

           
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>();
        }


        [Fact]
        public void WhenBookIdIsValid_Book_ShouldBeReturned()
        {
            
            var book = new Book { Title = "Query Test Book", PageCount = 300, PublishDate = DateTime.Now.Date.AddYears(-3), GenreId = 1 };
            _context.Books.Add(book);
            _context.SaveChanges();

            GetBookByIdQuery query = new GetBookByIdQuery(_context, _mapper);
            query.BookId = book.Id;

            
            var result = query.Handle();

           
            result.Should().NotBeNull();
            result.Title.Should().Be(book.Title);
        }
    }
}
