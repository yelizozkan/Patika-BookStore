using AutoMapper;
using BookStore.DbOperations;

namespace BookStore.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenreByIdQuery
    {
        public int GenreId { get; set; }
        public readonly IBookStoreDbContext _dbContext;
        public readonly IMapper _mapper;
        public GetGenreByIdQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public GenreByIdViewModel Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x => x.IsActive && x.Id == GenreId );
            if(genre is null)
                throw new InvalidOperationException("Genre not found");

            return _mapper.Map<GenreByIdViewModel>(genre); ;
        }

        public class GenreByIdViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
