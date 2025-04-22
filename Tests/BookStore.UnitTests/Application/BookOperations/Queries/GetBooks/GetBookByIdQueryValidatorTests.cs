using BookStore.Application.BookOperations.Queries.GetBooks;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Application.BookOperations.Queries.GetBooks
{
    public class GetBookByIdQueryValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenBookIdIsInvalid_Validator_ShouldReturnError(int bookId)
        {
            
            GetBookByIdQuery query = new GetBookByIdQuery(null, null);
            query.BookId = bookId;

            
            GetBookByIdQueryValidator validator = new GetBookByIdQueryValidator();
            var result = validator.Validate(query);

            
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void WhenBookIdIsValid_Validator_ShouldNotReturnError(int bookId)
        {
            
            GetBookByIdQuery query = new GetBookByIdQuery(null, null);
            query.BookId = bookId;

            
            GetBookByIdQueryValidator validator = new GetBookByIdQueryValidator();
            var result = validator.Validate(query);

            
            result.Errors.Count.Should().Be(0);
        }
    }
}
