using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PropertyRenting.Api.Models.Entities;

[Table("OwnerContract")]
public class OwnerContractEntity : BaseEntity
{
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

    [ForeignKey(name: nameof(BuildingId))]
    [JsonIgnore]
    public virtual BuildingEntity Building { get; set; }
    [ForeignKey(name: nameof(OwnerId))]
    [JsonIgnore]
    public virtual OwnerEntity Owner { get; set; }

    public virtual ICollection<OwnerFinancialTransactionEntity> OwnerFinancialTransactions { get; set; }
}
