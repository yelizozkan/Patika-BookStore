using FluentValidation;

namespace BookStore.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenreByIdQueryValidator : AbstractValidator<GetGenreByIdQuery>
    {
        public GetGenreByIdQueryValidator()
        {
            RuleFor(x => x.GenreId).GreaterThan(0).WithMessage("Genre Id must be greater than 0.");
        }
    }
}
