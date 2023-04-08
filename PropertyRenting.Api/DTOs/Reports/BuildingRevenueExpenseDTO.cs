namespace PropertyRenting.Api.DTOs.Reports;

public class BuildingRevenueExpenseDTO
{
    public Guid BuildingId { get; set; }
    public string BuildingName { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal TotalExpense { get; set; }
}