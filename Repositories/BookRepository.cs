using AutoMapper;
using BookAPI.Data;
using BookAPI.DTO;
using BookAPI.Models;
using BookAPI.Repositories;
using Microsoft.EntityFrameworkCore;

public class BookRepository : IBookRepository
{
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;

    public BookRepository(ApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task AddBook(BookDTO bookDto)
    {
        var book = _mapper.Map<Book>(bookDto);
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBook(int id)
    {

        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            throw new KeyNotFoundException($"Book with ID {id} not found.");
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
    }

    public async Task<BookDTO> GetBookById(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            throw new KeyNotFoundException($"Book with ID {id} not found.");
        }

        return _mapper.Map<BookDTO>(book);
    }

    public async Task<IEnumerable<BookDTO>> GetBooks()
    {
        var books = await _context.Books.ToListAsync();
        return _mapper.Map<IEnumerable<BookDTO>>(books);
    }

    public async Task UpdateBook(BookDTO bookDto)
    {
        var retrievedBook = await _context.Books.FindAsync(bookDto.BookID);

        if (retrievedBook == null)
        {
            throw new KeyNotFoundException($"Book with ID {bookDto.BookID} not found.");
        }

        _mapper.Map(bookDto, retrievedBook);
        await _context.SaveChangesAsync();
    }
}
