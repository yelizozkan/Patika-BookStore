using AutoMapper;
using BookStore.DbOperations;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public DeleteAuthorCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void Handle()
        {
            var author = _dbContext.Authors
           .Include(x => x.Books)                     
           .SingleOrDefault(x => x.Id == AuthorId);

            if (author is null)
                throw new InvalidOperationException("Author not found!");

            if (author.Books != null && author.Books.Any()) 
                throw new InvalidOperationException("The author cannot be deleted because they have published books!");

            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();
        }




    }
}
