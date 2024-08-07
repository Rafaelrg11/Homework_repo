using Homework.Models;

namespace Homework.DTOs
{
    public class AuthorDTO
    {
        public int IdAuthor { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public List<BooksDto2> Books { get; set; }

    }
}
