namespace PropertyRenting.Api.DTOs.Reports;

public class AccountBalanceDTO
{
    public Guid AccountId { get; set; }
    public string AccountCode { get; set; }
    public string AccountNameAR { get; set; }
    public string AccountNameEN { get; set; }
    public decimal BeginPeriodBalance { get; set; }
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
    public decimal EndPeriodBalance { get; set; }
}
