using FluentValidation;

namespace BookStore.BookOperations.GetBooks
{
    public class GetBookByIdQueryValidator : AbstractValidator<GetBookByIdQuery>
    {
        public GetBookByIdQueryValidator()
        {
            RuleFor(x => x.BookId).GreaterThan(0);
        }
    }
}
