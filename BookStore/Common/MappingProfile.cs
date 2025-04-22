using AutoMapper;
using BookStore.Application.BookOperations.Queries.GetBooks;
using BookStore.Application.GenreOperations.Queries.GetGenres;
using BookStore.Entities;
using static BookStore.Application.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;
using static BookStore.Application.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand;
using static BookStore.Application.AuthorOperations.Queries.GetAuthors.GetAuthorByIdQuery;
using static BookStore.Application.AuthorOperations.Queries.GetAuthors.GetAuthorsQuery;
using static BookStore.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using static BookStore.Application.BookOperations.Queries.GetBooks.GetBookByIdQuery;
using static BookStore.Application.GenreOperations.Queries.GetGenres.GetGenreByIdQuery;
using static BookStore.Application.GenreOperations.Queries.GetGenres.GetGenresQuery;

namespace BookStore.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           CreateMap<CreateBookModel, Book>();
           CreateMap<Book, BookViewModel>().ForMember(dest=> dest.Genre,opt => opt.MapFrom(src => src.Genre.Name));
           CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));

           CreateMap<Genre, GenresViewModel>();
           CreateMap<Genre, GenreByIdViewModel>();

           CreateMap<Author, AuthorsViewModel>();
           CreateMap<Author, AuthorViewModel>();

           CreateMap<CreateAuthorModel, Author>();       
           CreateMap<UpdateAuthorModel, Author>();
        }
    }
}
