namespace PropertyRenting.Api.DTOs.Reports;

public class OwnerBalanceDTO
{
    public Guid OwnerId { get; set; }
    public string OwnerNameAR { get; set; }
    public string OwnerNameEN { get; set; }
    public decimal BeginPeriodBalance { get; set; }
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
    public decimal EndPeriodBalance { get; set; }
}
