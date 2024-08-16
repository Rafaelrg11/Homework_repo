using Homework.DTOs;
using Homework.Models;
using Homework.Operations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuxiliarTableLoanController : ControllerBase
    {
        private AuxiliarTableLoanOperation _operation;

        private LoanOperation _LoanOpe;

        private BookOperation _bookope;

        private HomeworkContext _context;

        public AuxiliarTableLoanController(HomeworkContext context, BookOperation bookOperation, LoanOperation loan, AuxiliarTableLoanOperation operation)
        {
            _operation = operation;

            _bookope = bookOperation;

            _LoanOpe = loan;

            _context = context;
        }

        [HttpPut("UpdateAuxiliar/{idAuxiliar}")]
        public async Task<bool> UpdateAuxiliar(int idAuxiliar)
        {
            var result = await _operation.DeleteAuxiliar(idAuxiliar);

            return result;
        }


        [HttpDelete("DeleteAuxiliar/{idAuxiliar}")]
        public async Task<bool> DeleteAuxiliar(int idAuxiliar)
        {
            var result = await _operation.DeleteAuxiliar(idAuxiliar);

            return result;
        }


    }  
}
