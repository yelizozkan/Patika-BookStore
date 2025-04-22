using AutoMapper;
using BookStore.Application.BookOperations.Commands.CreateBook;
using BookStore.DbOperations;
using BookStore.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests
    {

        [Theory]
        [InlineData("Lord Of The Rings", 0, 0)]
        [InlineData("Lord Of The Rings", 100, 0)]
        [InlineData("", 0, 0)]
        [InlineData("", 100, 1)]
        [InlineData("Lord", 0, 1)]
        [InlineData("Lord", 100, 0)]
        [InlineData("Lord", 0, 0)]
        [InlineData(" ", 100, 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string title, int pageCount, int genreId)
        {
            // Arrange
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookCommand.CreateBookModel
            {
                Title = title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = genreId
            };

            // Act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldReturnError()
        {
            // Arrange
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookCommand.CreateBookModel
            {
                Title = "Lord Of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date, 
                GenreId = 1
            };

            // Act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            // Arrange
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookCommand.CreateBookModel
            {
                Title = "Lord Of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Today.AddYears(-1),
                GenreId = 1,
                AuthorId = 1
            };

            // Act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().Be(0);
        }


    }
}
