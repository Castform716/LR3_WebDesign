using System;
using System.Collections.Generic;

namespace LR3_WebDesign.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public string CardNumber { get; set; } = null!;

    public double? BalanceChange { get; set; }

    public DateTime? Date { get; set; }

    public string? Description { get; set; }

    public virtual Card CardNumberNavigation { get; set; } = null!;
}
