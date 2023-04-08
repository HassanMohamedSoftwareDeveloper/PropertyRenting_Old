using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyRenting.Api.Models.Entities;

[Table("ContractAdditions")]
public class ContractAdditionsEntity : BaseEntity
{
    public string NameAR { get; set; }
    public string NameEN { get; set; }
    public Guid? AccountId { get; set; }
}
