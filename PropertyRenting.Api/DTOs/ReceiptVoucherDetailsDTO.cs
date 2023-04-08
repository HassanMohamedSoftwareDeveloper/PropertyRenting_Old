namespace PropertyRenting.Api.DTOs;

public class ReceiptVoucherDetailsDTO
{
    public Guid Id { get; set; }
    public Guid ReceiptVoucherId { get; set; }
    public Guid? ExpenseId { get; set; }
    public Guid? InstallmentId { get; set; }
    public DateTime? DueDate { get; set; }
    public decimal? Installment { get; set; }
    public Guid? BuildingId { get; set; }
    public Guid? UnitId { get; set; }
    public decimal Amount { get; set; }


    public string Building { get; set; }
    public string Unit { get; set; }
    public string Expense { get; set; }
}
