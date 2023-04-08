namespace PropertyRenting.Api.DTOs;

public class ContractFinancialTransactionDTO
{
    public Guid Id { get; set; }
    public string ContractNumber { get; set; }
    public Guid ContractId { get; set; }
    public decimal Amount { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal Balance { get; set; }
    public Guid? ContractAdditionId { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsPaid { get; set; }
    public bool IsCancelled { get; set; }

    public string ContractAddition { get; set; }

    public Guid BuildingId { get; set; }
    public Guid UnitId { get; set; }
}
