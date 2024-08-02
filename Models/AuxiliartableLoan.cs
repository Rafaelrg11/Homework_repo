using System;
using System.Collections.Generic;

namespace Homework.Models;

public partial class AuxiliartableLoan
{
    public int IdAuxiliar { get; set; }

    public int? IdLoan { get; set; }

    public int? IdBook { get; set; }

    public virtual Book? IdBookNavigation { get; set; }

    public virtual Loan? IdLoanNavigation { get; set; }

}
