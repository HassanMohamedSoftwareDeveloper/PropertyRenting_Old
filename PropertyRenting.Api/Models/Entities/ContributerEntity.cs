using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyRenting.Api.Models.Entities;

[Table("Contributer")]
public class ContributerEntity : BaseEntity
{
    public string NameAR { get; set; }
    public string NameEN { get; set; }

    public virtual ICollection<BuildingContributerEntity> Buildings { get; set; }
}
