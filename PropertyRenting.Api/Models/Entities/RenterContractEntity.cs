using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PropertyRenting.Api.Models.Entities;

[Table("RenterContract")]

public class RenterContractEntity : BaseEntity
{
    public long AutoNumber { get; set; }
    public string ContractNumber { get; set; }
    public Guid UnitId { get; set; }
    public Guid RenterId { get; set; }
    public string Description { get; set; }
    public DateTime ContractDate { get; set; }
    public DateTime ContractStartDate { get; set; }
    public DateTime ContractEndDate { get; set; }
    public decimal ContractAmount { get; set; }
    public int ContractState { get; set; }
    public int PaymentMethod { get; set; }
    public bool Increasing { get; set; }
    public decimal? IncreasingValue { get; set; }
    [ForeignKey(name: nameof(UnitId))]
    [JsonIgnore]
    public virtual UnitEntity Unit { get; set; }
    [ForeignKey(name: nameof(RenterId))]
    [JsonIgnore]
    public virtual RenterEntity Renter { get; set; }

    public virtual ICollection<RenterFinancialTransactionEntity> RenterFinancialTransactions { get; set; }
}
