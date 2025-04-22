using AutoMapper;
using BookStore.DbOperations;
using static BookStore.Application.BookOperations.Queries.GetBooks.GetBookByIdQuery;

namespace BookStore.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAuthorsQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<AuthorsViewModel> Handle()
        {
            var authorList = _dbContext.Authors.OrderBy(x => x.Id).ToList();
            List<AuthorsViewModel> vm = _mapper.Map<List<AuthorsViewModel>>(authorList);
            return vm;
        }
        public class AuthorsViewModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public DateTime BirthDate { get; set; }
            public List<BookViewModel> Books { get; set; } = new List<BookViewModel>();
        }
    }
}
