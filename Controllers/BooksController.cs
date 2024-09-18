using BookAPI.DTO;
using BookAPI.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
        {
            var books = await _bookService.GetBooks();
            return Ok(books);
        }

        [HttpGet("{id:int}", Name = "GetBook")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookDTO>> GetBook(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID. ID must be greater than zero.");
            }
            var book = await _bookService.GetBookById(id);

            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BookDTO>> CreateBook([FromBody] BookDTO book)
        {
            if (book == null)
            {
                return BadRequest("Book object cannot be null.");
            }

            if (book.BookID > 0)
            {
                return BadRequest("Book ID must not be set when creating a new book.");
            }
            await _bookService.AddBook(book);
            return CreatedAtRoute("GetBook", new { id = book.BookID }, book);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDTO bookDto)
        {
            if (bookDto == null)
            {
                return BadRequest("Book cannot be null");
            }

            if (id != bookDto.BookID)
            {
                return BadRequest("ID in the URL does not match ID in the book object.");
            }

            var book = await _bookService.GetBookById(bookDto.BookID);
            if (book == null)
            {
                return NotFound("Book is not found");
            }

            await _bookService.UpdateBook(bookDto);
            return NoContent();
        }


        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PatchBook(int id, [FromBody] JsonPatchDocument<BookDTO> bookUpdates)
        {
            if (bookUpdates == null)
            {
                return BadRequest("Book cannot be null");
            }

            var book = await _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound("Book is not found");
            }

            bookUpdates.ApplyTo(book);
            await _bookService.UpdateBook(book);
            return NoContent();
        }

        [HttpDelete("{id:int}", Name = "DeleteBook")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteBook(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID. ID must be greater than zero.");
            }
            await _bookService.DeleteBook(id);
            return NoContent();
        }
    }
}
