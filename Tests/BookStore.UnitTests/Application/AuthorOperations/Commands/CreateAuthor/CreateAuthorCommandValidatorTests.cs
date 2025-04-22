using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTests
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("J.R.R.", "")]
        [InlineData("", "Tolkien")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnError(string name, string surname)
        {

            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorCommand.CreateAuthorModel
            {
                Name = name,
                Surname = surname,
                BirthDate = new DateTime(1892, 1, 3)
            };


            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);


            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {

            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorCommand.CreateAuthorModel
            {
                Name = "J.R.R.",
                Surname = "Tolkien",
                BirthDate = new DateTime(1892, 1, 3)
            };


            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}
