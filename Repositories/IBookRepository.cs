using BookAPI.DTO;


namespace BookAPI.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<BookDTO>> GetBooks();
        Task<BookDTO> GetBookById(int id);
        Task AddBook(BookDTO book);
        Task UpdateBook(BookDTO book);
        Task DeleteBook(int id);
    }
}
