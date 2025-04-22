using BookStore.Application.AuthorOperations.Commands.DeleteAuthor;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenAuthorIdIsInvalid_Validator_ShouldReturnError(int authorId)
        {
            // Arrange
            DeleteAuthorCommand command = new DeleteAuthorCommand(null, null);
            command.AuthorId = authorId;

            // Act
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void WhenAuthorIdIsValid_Validator_ShouldNotReturnError(int authorId)
        {
            // Arrange
            DeleteAuthorCommand command = new DeleteAuthorCommand(null, null);
            command.AuthorId = authorId;

            // Act
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
