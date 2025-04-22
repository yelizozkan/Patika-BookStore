using FluentValidation;

namespace BookStore.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorByIdQueryValidator : AbstractValidator<GetAuthorByIdQuery>
    {
        public GetAuthorByIdQueryValidator()
        {
            RuleFor(x => x.AuthorId).GreaterThan(0);
        }
    }
}
