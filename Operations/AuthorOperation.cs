using Homework.DTOs;
using Homework.Models;
using Microsoft.EntityFrameworkCore;

namespace Homework.Operations
{
    public class AuthorOperation
    {
        public HomeworkContext _context;

        public AuthorOperation(HomeworkContext context)
        {
            _context = context;
        }

        public async Task<List<Author>> GetAuthors()
        {
            var book = await _context.Authors.AsNoTracking().ToListAsync();

            return book;
        }

        public async Task<Author> GetAuthor(int id)
        {
            var result = await _context.Authors.FindAsync(id);

            return result;
        }

        public async Task<Author> CreateAuthor(Author author)
        {
            var result = await _context.Authors.AddAsync(author);

            await _context.SaveChangesAsync();

            return author;
        }

        public async Task<bool> UpdateAuthor(AuthorDTO author)
        {
            Author? author1 = await _context.Authors.FindAsync(author.IdAuthor);
            if (author1 != null)
            {
                author1.Name = author.Name;
                author1.Email = author.Email;
                
                await _context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<bool> DeleteAuthor(int id)
        {
            var result = await _context.Authors.FindAsync(id);
            if (result == null)
            {
                return false;
            }

            _context.Authors.Remove(result);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
