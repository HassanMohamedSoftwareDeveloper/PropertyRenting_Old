namespace PropertyRenting.Api.DTOs.Reports;

public class ContributorBalanceDTO
{
    public Guid ContributorId { get; set; }
    public string Contributor { get; set; }
    public decimal BeginPeriodBalance { get; set; }
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
    public decimal EndPeriodBalance { get; set; }
}
