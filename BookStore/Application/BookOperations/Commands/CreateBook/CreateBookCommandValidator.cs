using FluentValidation;

namespace BookStore.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(x => x.Model.GenreId).GreaterThan(0);
            RuleFor(x => x.Model.PageCount).GreaterThan(0);
            RuleFor(x => x.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(x => x.Model.Title).MinimumLength(10);
            RuleFor(x => x.Model.AuthorId).GreaterThan(0).WithMessage("AuthorId must be greater than zero.");

        }
    }
}
