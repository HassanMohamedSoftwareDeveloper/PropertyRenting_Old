namespace PropertyRenting.Api.DTOs;

public class OwnerContractDTO
{
    public Guid Id { get; set; }
    public long AutoNumber { get; set; }
    public string ContractNumber { get; set; }
    public Guid BuildingId { get; set; }
    public Guid OwnerId { get; set; }
    public string Description { get; set; }
    public DateTime ContractDate { get; set; }
    public DateTime ContractStartDate { get; set; }
    public DateTime ContractEndDate { get; set; }
    public decimal ContractAmount { get; set; }
    public int ContractState { get; set; }
    public int PaymentMethod { get; set; }
    public string Building { get; set; }
    public string Owner { get; set; }

    public List<ContractFinancialTransactionDTO> OwnerFinancialTransactions { get; set; }
}
