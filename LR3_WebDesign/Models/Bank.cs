using System;
using System.Collections.Generic;

namespace LR3_WebDesign.Models;

public partial class Bank
{
    public string Usreou { get; set; } = null!;

    public string? Name { get; set; }

    public string? Departments { get; set; }

    public double? OverallBalance { get; set; }

    public double? GoldCapacity { get; set; }

    public string? OriginCountry { get; set; }

    public virtual ICollection<Account> Accounts { get; } = new List<Account>();

    public virtual ICollection<Department> DepartmentsNavigation { get; } = new List<Department>();

    public virtual ICollection<InsuranceGiver> InsuranceGivers { get; } = new List<InsuranceGiver>();
}
