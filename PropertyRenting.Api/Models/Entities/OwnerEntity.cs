using System.ComponentModel.DataAnnotations.Schema;
namespace PropertyRenting.Api.Models.Entities;

[Table("Owner")]
public class OwnerEntity : BaseEntity
{
    public string NameAR { get; set; }
    public string NameEN { get; set; }
}
