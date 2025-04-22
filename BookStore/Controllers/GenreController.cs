using AutoMapper;
using BookStore.Application.GenreOperations.Commands.CreateGenre;
using BookStore.Application.GenreOperations.Commands.DeleteGenre;
using BookStore.Application.GenreOperations.Commands.UpdateGenre;
using BookStore.Application.GenreOperations.Queries.GetGenres;
using BookStore.DbOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using static BookStore.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand;
using static BookStore.Application.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        public readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GenreController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetGenreById(int id)
        {
            GetGenreByIdQuery query = new GetGenreByIdQuery(_context, _mapper);
            query.GenreId = id;

            GetGenreByIdQueryValidator validator = new GetGenreByIdQueryValidator();
            validator.ValidateAndThrow(query);

            var result = query.Handle();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
        {
            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = newGenre;

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();

        }

        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id , [FromBody] UpdateGenreModel updateGenre)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = id;
            command.Model = updateGenre;

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = id;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

    }
}
