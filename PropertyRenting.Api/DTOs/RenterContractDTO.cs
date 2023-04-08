namespace PropertyRenting.Api.DTOs;

public class RenterContractDTO
{
    public Guid Id { get; set; }
    public long AutoNumber { get; set; }
    public string ContractNumber { get; set; }
    public Guid UnitId { get; set; }
    public Guid RenterId { get; set; }
    public string Description { get; set; }
    public DateTime ContractDate { get; set; }
    public DateTime ContractStartDate { get; set; }
    public DateTime ContractEndDate { get; set; }
    public decimal ContractAmount { get; set; }
    public int ContractState { get; set; }
    public int PaymentMethod { get; set; }
    public bool Increasing { get; set; }
    public decimal? IncreasingValue { get; set; }
    public string UnitName { get; set; }
    public string UnitNumber { get; set; }
    public Guid BuildingId { get; set; }
    public string BuildingName { get; set; }
    public string Renter { get; set; }

    public List<ContractFinancialTransactionDTO> RenterFinancialTransactions { get; set; }
}
