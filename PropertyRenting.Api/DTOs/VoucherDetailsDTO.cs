namespace PropertyRenting.Api.DTOs;

public class VoucherDetailsDTO
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public decimal? DebitAmount { get; set; }
    public decimal? CreditAmount { get; set; }
    public Guid? RenterId { get; set; }
    public Guid? OwnerId { get; set; }
    public Guid? ContributerId { get; set; }
    public Guid? ExpenseId { get; set; }
    public Guid? BuildingId { get; set; }
    public Guid? UnitId { get; set; }
    public Guid? CashBankId { get; set; }

    public string Account { get; set; }
    public string Renter { get; set; }
    public string Owner { get; set; }
    public string Contributer { get; set; }
    public string Expense { get; set; }
    public string Building { get; set; }
    public string Unit { get; set; }
    public string CashBank { get; set; }
}
