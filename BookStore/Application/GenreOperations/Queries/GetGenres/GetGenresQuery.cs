using AutoMapper;
using BookStore.DbOperations;

namespace BookStore.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        public readonly IBookStoreDbContext _dbContext;
        public readonly IMapper _mapper;
        public GetGenresQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public List<GenresViewModel> Handle()
        {
            var genres = _dbContext.Genres.Where(x=>x.IsActive).OrderBy(x=> x.Id);
            List<GenresViewModel> returnObj = _mapper.Map<List<GenresViewModel>>(genres);
            return returnObj;
        }

        public class GenresViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
