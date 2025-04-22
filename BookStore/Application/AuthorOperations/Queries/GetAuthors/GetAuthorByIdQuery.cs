using AutoMapper;
using BookStore.DbOperations;
using static BookStore.Application.BookOperations.Queries.GetBooks.GetBookByIdQuery;

namespace BookStore.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorByIdQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public int AuthorId { get; set; }
        public GetAuthorByIdQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public AuthorViewModel Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (author is null)
                throw new InvalidOperationException("The author does not exist");
            AuthorViewModel viewModel = _mapper.Map<AuthorViewModel>(author);
            return viewModel;
        }

        public class AuthorViewModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public DateTime BirthDate { get; set; }
            public List<BookViewModel> Books { get; set; } = new List<BookViewModel>();
        }
    }
}
