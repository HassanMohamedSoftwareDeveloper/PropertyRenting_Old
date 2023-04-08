namespace PropertyRenting.Api.DTOs.Reports;

public class UnitBalanceDTO
{
    public Guid UnitId { get; set; }
    public string UnitNumber { get; set; }
    public string UnitName { get; set; }
    public string BuildingName { get; set; }
    public decimal BeginPeriodBalance { get; set; }
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
    public decimal EndPeriodBalance { get; set; }
}
