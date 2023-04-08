namespace PropertyRenting.Api.DTOs.Reports;

public class RenterBalanceDTO
{
    public Guid RenterId { get; set; }
    public string RenterNameAR { get; set; }
    public string RenterNameEN { get; set; }
    public decimal BeginPeriodBalance { get; set; }
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
    public decimal EndPeriodBalance { get; set; }
}
