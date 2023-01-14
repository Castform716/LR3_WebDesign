using System;
using System.Collections.Generic;

namespace LR3_WebDesign.Models;

public partial class Customer
{
    public string Itn { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Mfo { get; set; }

    public string LegalEntityIndividual { get; set; } = null!;

    public int? TrustLimit { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Adress { get; set; }

    public int? Postcode { get; set; }

    public virtual ICollection<Account> Accounts { get; } = new List<Account>();

    public virtual ICollection<Card> Cards { get; } = new List<Card>();
}
