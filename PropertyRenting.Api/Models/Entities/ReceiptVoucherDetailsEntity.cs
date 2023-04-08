using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PropertyRenting.Api.Models.Entities;
[Table("ReceiptVoucherDetails")]
public class ReceiptVoucherDetailsEntity : BaseEntity
{
    public Guid ReceiptVoucherId { get; set; }
    public Guid? InstallmentId { get; set; }
    public DateTime? DueDate { get; set; }
    public decimal? Installment { get; set; }
    public Guid? BuildingId { get; set; }
    public Guid? UnitId { get; set; }
    public Guid? ExpenseId { get; set; }
    public decimal Amount { get; set; }

    [JsonIgnore]
    [ForeignKey(name: nameof(ReceiptVoucherId))]
    public virtual ReceiptVoucherEntity ReceiptVoucher { get; set; }
    [JsonIgnore]
    [ForeignKey(name: nameof(BuildingId))]
    public virtual BuildingEntity Building { get; set; }
    [JsonIgnore]
    [ForeignKey(name: nameof(UnitId))]
    public virtual UnitEntity Unit { get; set; }
    [JsonIgnore]
    [ForeignKey(name: nameof(ExpenseId))]
    public virtual ExpenseEntity Expense { get; set; }
}
