using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PropertyRenting.Api.Models.Entities;

[Table("City")]
public class CityEntity : BaseEntity
{
    public string NameAR { get; set; }
    public string NameEN { get; set; }
    public Guid CountryId { get; set; }
    [JsonIgnore]
    [ForeignKey(name: nameof(CountryId))]
    public virtual CountryEntity Country { get; set; }
    public virtual ICollection<DistrictEntity> Districts { get; set; }
}
