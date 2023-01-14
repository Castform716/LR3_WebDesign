using System;
using System.Collections.Generic;

namespace LR3_WebDesign.Models;

public partial class InsuranceGiver
{
    public string InsuranceUsreou { get; set; } = null!;

    public string? BankCountry { get; set; }

    public double? InsuranceAmount { get; set; }

    public string InsuranceObject { get; set; } = null!;

    public bool IsBank { get; set; }

    public virtual Bank InsuranceObjectNavigation { get; set; } = null!;
}
