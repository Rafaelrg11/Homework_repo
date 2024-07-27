﻿using Homework.Models;

namespace Homework.DTOs
{
    public class BookDTO
    {
        public int IdBook { get; set; }

        public int? IdAutor { get; set; }

        public string? Name { get; set; }

        public string? Gender { get; set; }

        public int? NumPags { get; set; }

        public string? Available { get; set; }

        public virtual Author? IdAutorNavigation { get; set; }

        public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
