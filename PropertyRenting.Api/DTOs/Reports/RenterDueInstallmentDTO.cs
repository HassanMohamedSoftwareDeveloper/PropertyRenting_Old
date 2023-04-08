namespace PropertyRenting.Api.DTOs.Reports;

public class RenterDueInstallmentDTO
{
    public long AutoNumber { get; set; }
    public string ContractNumber { get; set; }
    public string TypeAR { get; set; }
    public string TypeEN { get; set; }
    public decimal Amount { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal Balance { get; set; }
    public DateTime DueDate { get; set; }
    public string RenterAR { get; set; }
    public string RenterEN { get; set; }
    public string BuildingName { get; set; }
    public string UnitName { get; set; }
    public string UnitNumber { get; set; }
}
