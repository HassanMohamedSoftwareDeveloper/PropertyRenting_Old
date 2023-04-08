using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyRenting.Api.Models.Entities;

[Table("CashBank")]
public class CashBankEntity : BaseEntity
{
    public string Name { get; set; }
    public int TypeId { get; set; }
    public Guid AccountId { get; set; }
}
