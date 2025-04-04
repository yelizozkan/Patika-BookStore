﻿using AutoMapper;
using BookStore.Common;
using BookStore.DbOperations;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BookOperations.GetBooks
{
    public class GetBookByIdQuery
    {
        public int BookId { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBookByIdQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BookViewModel Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book == null)
                throw new InvalidOperationException("The book does not exist");

            BookViewModel viewModel = _mapper.Map<BookViewModel>(book);

            return viewModel;
        }

        public class BookViewModel
        {
            public string Title { get; set; }
            public string Genre { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}
