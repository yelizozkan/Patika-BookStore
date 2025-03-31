using BookStore.DbOperations;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;


        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }

        //private static List<Book> BookList = new List<Book>()
        //{
        //    new Book() {
        //        Id = 1,
        //        Title = "The Great Gatsby",
        //        GenreId = 1,
        //        PageCount = 180,
        //        PublishDate = new DateTime(1925, 4, 10)
        //    },

        //    new Book() {
        //        Id = 2,
        //        Title = "Herland",
        //        GenreId = 2,
        //        PageCount = 250,
        //        PublishDate = new DateTime(2010, 5, 10)
        //    },

        //    new Book() {
        //        Id = 3,
        //        Title = "Dune",
        //        GenreId = 3,
        //        PageCount = 540,
        //        PublishDate = new DateTime(2001, 12, 21)
        //    }
        //};

        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = _context.Books.OrderBy(x => x.Id).ToList<Book>();
            return bookList;
        }

        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            var book = _context.Books.Where(book => book.Id == id).SingleOrDefault();
            return book;
        }

        //[HttpGet]
        //public Book Get([FromQuery] string id)
        //{
        //    var book = BookList.Where(book => book.Id ==Convert.ToInt32(id)).SingleOrDefault();
        //    return book;
        //}


        [HttpPost]
        public IActionResult AddBook([FromBody] Book newbook)
        {
            var book = _context.Books.SingleOrDefault(x => x.Title == newbook.Title);

            if(book != null)
            {
                return BadRequest();
            }

            _context.Books.Add(newbook);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (book == null)
            {
                return BadRequest();
            }

            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount ;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;

            _context.SaveChanges();
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (book == null)
            {
                return BadRequest();
            }

            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }


    }
}
