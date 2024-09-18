using BookAPI.DTO;
using BookAPI.Repositories;

namespace BookAPI.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;


        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<BookDTO>> GetBooks()
        {
            var books = await _bookRepository.GetBooks();
            return books;
        }

        public async Task<BookDTO> GetBookById(int id)
        {

            var book = await _bookRepository.GetBookById(id);
            return book;

        }

        public async Task AddBook(BookDTO bookDto)
        {

            await _bookRepository.AddBook(bookDto);

        }

        public async Task UpdateBook(BookDTO bookDto)
        {

            await _bookRepository.UpdateBook(bookDto);

        }

        public async Task DeleteBook(int id)
        {
            await _bookRepository.DeleteBook(id);
        }
    }
}
