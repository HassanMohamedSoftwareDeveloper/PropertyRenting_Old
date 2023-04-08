using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace PropertyRenting.Api.Models.Entities;

[Table("District")]
public class DistrictEntity : BaseEntity
{
    public string NameAR { get; set; }
    public string NameEN { get; set; }
    public Guid CityId { get; set; }
    [JsonIgnore]
    [ForeignKey(name: nameof(CityId))]
    public virtual CityEntity City { get; set; }
    public virtual ICollection<UnitEntity> Units { get; set; }
    public virtual ICollection<BuildingEntity> Buildings { get; set; }
}
