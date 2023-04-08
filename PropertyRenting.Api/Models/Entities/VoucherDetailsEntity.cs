using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PropertyRenting.Api.Models.Entities;
[Table("VoucherDetails")]
public class VoucherDetailsEntity : BaseEntity
{
    public Guid VoucherId { get; set; }
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
    public Guid? InstallmentId { get; set; }

    [JsonIgnore]
    [ForeignKey(name: nameof(VoucherId))]
    public virtual VoucherEntity Voucher { get; set; }
    [JsonIgnore]
    [ForeignKey(name: nameof(AccountId))]
    public virtual AccountEntity Account { get; set; }
    [JsonIgnore]
    [ForeignKey(name: nameof(RenterId))]
    public virtual RenterEntity Renter { get; set; }
    [JsonIgnore]
    [ForeignKey(name: nameof(OwnerId))]
    public virtual OwnerEntity Owner { get; set; }
    [JsonIgnore]
    [ForeignKey(name: nameof(ContributerId))]
    public virtual ContributerEntity Contributer { get; set; }
    [JsonIgnore]
    [ForeignKey(name: nameof(ExpenseId))]
    public virtual ExpenseEntity Expense { get; set; }
    [JsonIgnore]
    [ForeignKey(name: nameof(BuildingId))]
    public virtual BuildingEntity Building { get; set; }
    [JsonIgnore]
    [ForeignKey(name: nameof(UnitId))]
    public virtual UnitEntity Unit { get; set; }
    [JsonIgnore]
    [ForeignKey(name: nameof(CashBankId))]
    public virtual CashBankEntity CashBank { get; set; }
}
