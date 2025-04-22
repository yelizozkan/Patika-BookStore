using BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTests
    {
        [Theory]
        [InlineData(0, "", "")]
        [InlineData(-1, "", "")]
        [InlineData(0, "George", "")]
        [InlineData(0, "", "Orwell")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnError(int authorId, string name, string surname)
        {

            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.AuthorId = authorId;
            command.Model = new UpdateAuthorCommand.UpdateAuthorModel
            {
                Name = name,
                Surname = surname,
                BirthDate = new DateTime(1903, 6, 25)
            };

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {

            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.AuthorId = 1;
            command.Model = new UpdateAuthorCommand.UpdateAuthorModel
            {
                Name = "George",
                Surname = "Orwell",
                BirthDate = new DateTime(1903, 6, 25)
            };

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}
