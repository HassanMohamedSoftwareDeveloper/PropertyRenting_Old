using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PropertyRenting.Api.Models.Entities;

[Table("AccountSetup")]
public class AccountSetupEntity : BaseEntity
{
    public Guid AccruedRevenueAccountId { get; set; }
    public Guid RevenueAccountId { get; set; }
    public Guid AccruedExpenseAccountId { get; set; }
    public Guid ExpenseAccountId { get; set; }
    public Guid ContributerAccountId { get; set; }
    public Guid RenterAccountId { get; set; }
    public Guid OwnerAccountId { get; set; }


    [ForeignKey(name: nameof(AccruedRevenueAccountId))]
    [JsonIgnore]
    public virtual AccountEntity AccruedRevenueAccount { get; set; }
    [ForeignKey(name: nameof(RevenueAccountId))]
    [JsonIgnore]
    public virtual AccountEntity RevenueAccount { get; set; }

    [ForeignKey(name: nameof(AccruedExpenseAccountId))]
    [JsonIgnore]
    public virtual AccountEntity AccruedExpenseAccount { get; set; }
    [ForeignKey(name: nameof(ExpenseAccountId))]
    [JsonIgnore]
    public virtual AccountEntity ExpenseAccount { get; set; }

    [ForeignKey(name: nameof(ContributerAccountId))]
    [JsonIgnore]
    public virtual AccountEntity ContributerAccount { get; set; }
    [ForeignKey(name: nameof(RenterAccountId))]
    [JsonIgnore]
    public virtual AccountEntity RenterAccount { get; set; }
    [ForeignKey(name: nameof(OwnerAccountId))]
    [JsonIgnore]
    public virtual AccountEntity OwnerAccount { get; set; }
}
