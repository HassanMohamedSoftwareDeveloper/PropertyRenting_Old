namespace PropertyRenting.Api.DTOs.Reports;

public class RenterDueInstallmentDTO
{
    public DateTime ContractStartDate { get; set; }
    public string ContractEndDate { get; set; }
    public string TypeAR { get; set; }
    public decimal Amount { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal Balance { get; set; }
    public DateTime DueDate { get; set; }
    public int RemainingDays { get; set; }
    public string RenterAR { get; set; }
    public string MobileNumber { get; set; }
    public string UnitNumber { get; set; }
}
