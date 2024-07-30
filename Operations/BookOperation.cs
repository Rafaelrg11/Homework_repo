using Homework.DTOs;
using Homework.Models;
using Microsoft.EntityFrameworkCore;

namespace Homework.Operations
{
    public class BookOperation
    {
        public HomeworkContext _context;

        public BookOperation(HomeworkContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetBooks()
        {
            var book = await _context.Books.AsNoTracking().ToListAsync();

            return book;
        } 

        public async Task<Book> GetBook(int idBook)
        {
            var result = await _context.Books.FindAsync(idBook);

            return result;
        }

        public async Task<Book> CreateBook(Book book)
        {
            var result = await _context.Books.AddAsync(book);

            await _context.SaveChangesAsync();

            return book;
        }

        public async Task<bool> UpdateBook(BookDTO bookdto)
        {
            Book? book = await _context.Books.FindAsync(bookdto.IdBook);
            if (book != null) 
            {
                book.Name = bookdto.Name;
                book.NumPags = bookdto.NumPags;
                book.Gender = bookdto.Gender;
                book.Available = bookdto.Available;
                book.IdAutor = bookdto.IdAutor;              
                    
                await _context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<bool> DeleteBook(int idBook)
        {
            var result = await _context.Books.FindAsync(idBook);
            if (result != null)
            {
                return false;
            }

            _context.Books.Remove(result);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
