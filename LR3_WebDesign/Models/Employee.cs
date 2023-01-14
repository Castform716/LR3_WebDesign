using System;
using System.Collections.Generic;

namespace LR3_WebDesign.Models;

public partial class Employee
{
    public string Itn { get; set; } = null!;

    public int Department { get; set; }

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public double Sallary { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int? PostCode { get; set; }

    public string? Job { get; set; }

    public virtual Department DepartmentNavigation { get; set; } = null!;
}
