using System.ComponentModel.DataAnnotations.Schema;
namespace PropertyRenting.UI.Models.Entities;

[Table("Owner")]
public class OwnerEntity : BaseEntity
{
    public string NameAR { get; set; }
    public string NameEN { get; set; }

    public virtual ICollection<BuildingOwnerEntity> Buildings { get; set; }
}
