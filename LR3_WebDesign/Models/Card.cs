using System;
using System.Collections.Generic;

namespace LR3_WebDesign.Models;

public partial class Card
{
    public string CardNumber { get; set; } = null!;

    public string Itn { get; set; } = null!;

    public string? TypeOfCard { get; set; }

    public DateTime DateOfExpire { get; set; }

    public int? Cvv { get; set; }

    public double? Percentage { get; set; }

    public virtual Customer ItnNavigation { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; } = new List<Transaction>();
}
