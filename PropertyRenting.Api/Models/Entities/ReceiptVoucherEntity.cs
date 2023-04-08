using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PropertyRenting.Api.Models.Entities;
[Table("ReceiptVoucher")]
public class ReceiptVoucherEntity : BaseEntity
{
    public int SanadTypeId { get; set; }
    public long AutoNumber { get; set; }
    public string SanadNumber { get; set; }
    public DateTime SanadDate { get; set; }
    public Guid CashBankId { get; set; }
    public Guid? OwnerId { get; set; }
    public Guid? RenterId { get; set; }
    public Guid? ContributerId { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public int StateId { get; set; }

    [JsonIgnore]
    [ForeignKey(name: nameof(CashBankId))]
    public virtual CashBankEntity CashBank { get; set; }
    [JsonIgnore]
    [ForeignKey(name: nameof(OwnerId))]
    public virtual OwnerEntity Owner { get; set; }
    [JsonIgnore]
    [ForeignKey(name: nameof(RenterId))]
    public virtual RenterEntity Renter { get; set; }
    [JsonIgnore]
    [ForeignKey(name: nameof(ContributerId))]
    public virtual ContributerEntity Contributer { get; set; }
    public virtual ICollection<ReceiptVoucherDetailsEntity> SanadDetails { get; set; }
}
