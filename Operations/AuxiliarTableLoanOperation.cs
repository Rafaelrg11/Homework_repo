using Homework.DTOs;
using Homework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Homework.Operations
{
    public class AuxiliarTableLoanOperation
    {
        public HomeworkContext _context;

        public AuxiliarTableLoanOperation(HomeworkContext context)
        {
            _context = context;
        }

        public async Task<List<AuxiliartableLoan>> GetAuxiliarLoans()
        {
            var ope = await _context.AuxiliartableLoans.AsNoTracking().ToListAsync();

            return ope;
        }

        public async Task<AuxiliartableLoan> GetAuxiliartableLoan(int idAuxiliarLoan)
        {
            var ope = await _context.AuxiliartableLoans.FindAsync(idAuxiliarLoan);

            return ope;
        }
         
        public async Task<AuxiliartableLoan> CreateAuxiliar(AuxiliartableLoan auxiliartable)
        {
            var ope = await _context.AuxiliartableLoans.AddAsync(auxiliartable);

            await _context.SaveChangesAsync();

            return auxiliartable;
        }
    }
}
