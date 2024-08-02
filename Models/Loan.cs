using System;
using System.Collections.Generic;

namespace Homework.Models;

public partial class Loan
{
    public int IdLoan { get; set; }

    public DateTime? DateLoan { get; set; } 

    public DateTime? DateLoanCompletion { get; set; } 

    public virtual ICollection<AuxiliartableLoan> AuxiliarTable { get; set; } = new List<AuxiliartableLoan>();
}
