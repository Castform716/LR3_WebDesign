namespace LR3_WebDesign.Models
{
    public class AccountDTO
    {
        public string AccountNumber { get; set; } = null!;

        public string Usreou { get; set; } = null!;

        public string Itn { get; set; } = null!;

        public string? Currency { get; set; }

        public double Balance { get; set; }

        public double? CreditSum { get; set; }

        public string? BankName { get; set; }

        public string? CustomerName { get; set; }
    }
}
