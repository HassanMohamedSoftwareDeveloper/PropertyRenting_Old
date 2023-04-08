namespace PropertyRenting.Api.DTOs.Reports;

public class CashBankBalanceDTO
{
    public Guid CashBankId { get; set; }
    public string CashBankName { get; set; }
    public decimal BeginPeriodBalance { get; set; }
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
    public decimal EndPeriodBalance { get; set; }
}
