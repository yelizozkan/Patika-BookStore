using AutoMapper;
using BookStore.BookOperations.GetBooks;
using BookStore.Entities;
using static BookStore.BookOperations.CreateBook.CreateBookCommand;
using static BookStore.BookOperations.GetBooks.GetBookByIdQuery;

namespace BookStore.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           CreateMap<CreateBookModel, Book>();
           CreateMap<Book, BookViewModel>().ForMember(dest=> dest.Genre,opt=>opt.MapFrom(src=>((GenreEnum)src.GenreId).ToString()));
           CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
        }
    }
}
