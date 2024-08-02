using System;
using System.Collections.Generic;

namespace Homework.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<AuxiliartableLoan> AuxiliarTable { get; set; } = new List<AuxiliartableLoan>();

}
