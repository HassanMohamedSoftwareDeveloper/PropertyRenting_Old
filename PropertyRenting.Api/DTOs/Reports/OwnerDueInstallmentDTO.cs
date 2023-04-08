namespace PropertyRenting.Api.DTOs.Reports;

public class OwnerDueInstallmentDTO
{
    public long AutoNumber { get; set; }
    public string ContractNumber { get; set; }
    public decimal Amount { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal Balance { get; set; }
    public DateTime DueDate { get; set; }
    public string OwnerAR { get; set; }
    public string OwnerEN { get; set; }
    public string BuildingName { get; set; }
}
