using BookStore.Application.BookOperations.Commands.DeleteBook;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidBookIdIsGiven_Validator_ShouldReturnError(int bookId)
        {
            // Arrange
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = bookId;

            // Act
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void WhenValidBookIdIsGiven_Validator_ShouldNotReturnError(int bookId)
        {
            // Arrange
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = bookId;

            // Act
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
