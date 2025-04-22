using BookStore.Application.GenreOperations.Queries.GetGenres;
using FluentValidation;

namespace BookStore.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator()
        {
            RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(2).MaximumLength(50)
                .WithMessage("Genre name must be between 2 and 50 characters.");
        }
    }
}
