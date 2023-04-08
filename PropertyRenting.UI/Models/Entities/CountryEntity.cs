using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyRenting.UI.Models.Entities;

[Table("Country")]
public class CountryEntity : BaseEntity
{
    public string NameAR { get; set; }
    public string NameEN { get; set; }
    public virtual ICollection<CityEntity> Cities { get; set; }
}
