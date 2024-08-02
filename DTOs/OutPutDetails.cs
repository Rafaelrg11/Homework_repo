namespace Homework.DTOs
{
    public class OutPutLoanDetails
    {
        public int idLoan {  get; set; }

        public List<OutputBooks> Books { get; set; }
    }

    public class  OutputBooks
    {
        public int IdBook { get; set; }

        public string? namebook { get; set; }

        public string? Genrer { get; set; }        


    }
}
