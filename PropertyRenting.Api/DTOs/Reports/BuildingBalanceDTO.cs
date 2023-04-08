namespace PropertyRenting.Api.DTOs.Reports;

public class BuildingBalanceDTO
{
    public Guid BuildingId { get; set; }
    public string BuildingName { get; set; }
    public decimal BeginPeriodBalance { get; set; }
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
    public decimal EndPeriodBalance { get; set; }
}
