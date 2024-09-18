using BookAPI.DTO;

namespace BookAPI.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookDTO>> GetBooks();
        Task<BookDTO> GetBookById(int id);
        Task AddBook(BookDTO bookDto);
        Task UpdateBook(BookDTO bookDto);
        Task DeleteBook(int id);
    }
}
