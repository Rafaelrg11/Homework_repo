using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace Homework.DTOs
{
    public class LoanCustom
    {      
        public int idBook {  get; set; }  
        
        public int idUser { get; set; }

        public string? Name { get; set; } 

        public string? Genrer { get; set; }

    }
}
