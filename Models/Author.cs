using System;
using System.Collections.Generic;

namespace Homework.Models;

public partial class Author
{
    public int IdAuthor { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
