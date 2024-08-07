namespace Homework.DTOs
{
    public class OutputBook
    {
        public string nameBook { get; set; }

        public string genderBook { get; set; }

        public int numPags { get; set; }

        public Autors OutputAutor { get; set; }
    }

    public class Autors
    {
        public string nameAutor { get; set; }

        public string emailAutor { get; set; }
    }
}
