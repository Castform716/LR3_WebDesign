using System;
using System.Collections.Generic;

namespace LR3_WebDesign.Models;

public partial class Account
{
    public string AccountNumber { get; set; } = null!;

    public string Usreou { get; set; } = null!;

    public string Itn { get; set; } = null!;

    public string? Currency { get; set; }

    public double Balance { get; set; }

    public double? CreditSum { get; set; }

    //public virtual Customer ItnNavigation { get; set; } = null!;

    //public virtual Bank UsreouNavigation { get; set; } = null!;
}
