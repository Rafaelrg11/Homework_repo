using Homework.Models;

namespace Homework.DTOs
{
    public class AuthorDTO
    {
        public int IdAuthor { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
