using BookStore.DbOperations;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BookOperations.GetBookById
{
    public class GetBookByIdQuery
    {
        public int BookId { get; set; }
        private readonly BookStoreDbContext _dbContext;

        public GetBookByIdQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BookViewModel Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book == null)
                throw new InvalidOperationException("The book does not exist");

            BookViewModel viewModel = new BookViewModel();
            viewModel.Title = book.Title;
            viewModel.GenreId = book.GenreId;
            viewModel.PageCount = book.PageCount;
            viewModel.PublishDate = book.PublishDate;

            return viewModel;
        }

        public class BookViewModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}
