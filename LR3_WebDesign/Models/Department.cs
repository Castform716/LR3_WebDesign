using System;
using System.Collections.Generic;

namespace LR3_WebDesign.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string FromBank { get; set; } = null!;

    public string? Specialization { get; set; }

    public string? WorkDays { get; set; }

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();

    public virtual Bank FromBankNavigation { get; set; } = null!;
}
