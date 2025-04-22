using BookStore.Application.GenreOperations.Queries.GetGenres;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenreByIdQueryValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenGenreIdIsInvalid_Validator_ShouldReturnError(int genreId)
        {

            GetGenreByIdQuery query = new GetGenreByIdQuery(null, null);
            query.GenreId = genreId;

            GetGenreByIdQueryValidator validator = new GetGenreByIdQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void WhenGenreIdIsValid_Validator_ShouldNotReturnError(int genreId)
        {

            GetGenreByIdQuery query = new GetGenreByIdQuery(null, null);
            query.GenreId = genreId;

            GetGenreByIdQueryValidator validator = new GetGenreByIdQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().Be(0);
        }
    }
}
