using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PropertyRenting.Api.Models.Entities;
[Table("OwnerFinancialTransaction")]
public class OwnerFinancialTransactionEntity : BaseEntity
{
    public Guid ContractId { get; set; }
    public decimal Amount { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal Balance { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsPaid { get; set; }
    public bool IsCancelled { get; set; }


    [ForeignKey(name: nameof(ContractId))]
    [JsonIgnore]
    public virtual OwnerContractEntity OwnerContract { get; set; }
}
