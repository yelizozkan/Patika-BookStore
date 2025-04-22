using BookStore.Application.BookOperations.Commands.UpdateBook;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests
    {
        [Theory]
        [InlineData(0, "")]
        [InlineData(-1, "")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnError(int bookId, string title)
        {

            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = bookId;
            command.Model = new UpdateBookCommand.UpdateBookModel
            {
                Title = title,
                PageCount = 100,
                GenreId = 1
            };


            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);


            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = 1;
            command.Model = new UpdateBookCommand.UpdateBookModel
            {
                Title = "Valid Book Title",
                PageCount = 100,
                GenreId = 1,
                PublishDate = DateTime.Now.Date.AddYears(-1) 
            };

            
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);


            result.Errors.Count.Should().Be(0);
        }

    }
}
