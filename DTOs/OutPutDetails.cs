namespace Homework.DTOs
{
    public class OutPutLoanDetails
    {

        public DateTime DateLoan { get; set; } = DateTime.Now;
        public DateTime DateLoanCompletion { get; set; } = DateTime.Now;
        public List<OutputBooks> Books { get; set; }
        public OutputUser? User { get; set; }

    }

    public class OutputBooks
    {
        public int idAuxiliar { get; set; }
        public string namebook { get; set; }
        public string Genrer { get; set; }
        public List<OutputAuthors> Authors { get; set; }

    }

    public class OutputAuthors
    {
        public string NameAuthor { get; set; }
        public string EmailAuthor { get; set; }
    }

    public class OutputUser
    {
        public string NameUser { get; set; }
        public string EmailUser { get; set; }
    }
}
