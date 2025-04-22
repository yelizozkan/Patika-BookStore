using AutoMapper;
using BookStore.DbOperations;
using BookStore.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static BookStore.Application.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;
using static BookStore.Application.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand;
using static BookStore.Application.AuthorOperations.Queries.GetAuthors.GetAuthorByIdQuery;
using static BookStore.Application.AuthorOperations.Queries.GetAuthors.GetAuthorsQuery;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public AuthorController(BookStoreDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            var authors = _context.Authors
            .Include(x => x.Books) 
            .ToList();
            return Ok(_mapper.Map<List<AuthorsViewModel>>(authors));
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthorById(int id)
        {
            var author = _context.Authors
            .Include(x => x.Books)                     
            .SingleOrDefault(x => x.Id == id);

            if (author is null)
                return NotFound();

            return Ok(_mapper.Map<AuthorViewModel>(author));
        }

        [HttpPost]
        public IActionResult CreateAuthor([FromBody] CreateAuthorModel newAuthor)
        {
            var author = _mapper.Map<Author>(newAuthor);
            _context.Authors.Add(author);
            _context.SaveChanges();

            var authorViewModel = _mapper.Map<AuthorViewModel>(author);
            return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, authorViewModel);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel updatedAuthor)
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == id);
            if (author is null)
                return NotFound();
            _mapper.Map(updatedAuthor, author);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            var author = _context.Authors
                .Include(x => x.Books)                            
                .SingleOrDefault(x => x.Id == id);

            if (author is null)
                return NotFound();

            if (author.Books != null && author.Books.Any())        
                return BadRequest("The author cannot be deleted because they have published books!");

            _context.Authors.Remove(author);
            _context.SaveChanges();
            return NoContent();
        }




    }
}
