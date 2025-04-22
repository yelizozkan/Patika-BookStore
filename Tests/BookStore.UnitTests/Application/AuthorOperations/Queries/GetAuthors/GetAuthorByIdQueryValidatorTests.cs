using BookStore.Application.AuthorOperations.Queries.GetAuthors;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorByIdQueryValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenAuthorIdIsInvalid_Validator_ShouldReturnError(int authorId)
        {

            GetAuthorByIdQuery query = new GetAuthorByIdQuery(null, null);
            query.AuthorId = authorId;


            GetAuthorByIdQueryValidator validator = new GetAuthorByIdQueryValidator();
            var result = validator.Validate(query);


            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void WhenAuthorIdIsValid_Validator_ShouldNotReturnError(int authorId)
        {

            GetAuthorByIdQuery query = new GetAuthorByIdQuery(null, null);
            query.AuthorId = authorId;


            GetAuthorByIdQueryValidator validator = new GetAuthorByIdQueryValidator();
            var result = validator.Validate(query);


            result.Errors.Count.Should().Be(0);
        }
    }
}
