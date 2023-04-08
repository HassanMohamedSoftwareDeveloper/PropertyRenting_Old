using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyRenting.Api.Models.Entities;

[Table("Country")]
public class CountryEntity : BaseEntity
{
    public string Code { get; set; }
    public string NameAR { get; set; }
    public string NameEN { get; set; }
    public string NationalityAR { get; set; }
    public string NationalityEN { get; set; }
    public virtual ICollection<CityEntity> Cities { get; set; }
}
