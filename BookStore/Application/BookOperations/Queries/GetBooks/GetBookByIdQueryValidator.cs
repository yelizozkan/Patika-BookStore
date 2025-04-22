using FluentValidation;

namespace BookStore.Application.BookOperations.Queries.GetBooks
{
    public class GetBookByIdQueryValidator : AbstractValidator<GetBookByIdQuery>
    {
        public GetBookByIdQueryValidator()
        {
            RuleFor(x => x.BookId).GreaterThan(0);
        }
    }
}
