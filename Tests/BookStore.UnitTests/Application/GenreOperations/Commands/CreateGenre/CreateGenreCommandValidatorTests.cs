using BookStore.Application.GenreOperations.Commands.CreateGenre;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnError(string name)
        {
            CreateGenreCommand command = new CreateGenreCommand(null);
            command.Model = new CreateGenreCommand.CreateGenreModel
            {
                Name = name
            };


            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {

            CreateGenreCommand command = new CreateGenreCommand(null);
            command.Model = new CreateGenreCommand.CreateGenreModel
            {
                Name = "Science Fiction"
            };

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);


            result.Errors.Count.Should().Be(0);
        }
    }
}
