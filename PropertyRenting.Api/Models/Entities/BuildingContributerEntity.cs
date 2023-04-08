using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PropertyRenting.Api.Models.Entities;

[Table("BuildingContributer")]
public class BuildingContributerEntity : BaseEntity
{
    public Guid ContributerId { get; set; }
    public Guid BuildingId { get; set; }
    [Precision(2)]
    public decimal Percentage { get; set; }

    [JsonIgnore]
    [ForeignKey(name: nameof(ContributerId))]
    public virtual ContributerEntity Contributer { get; set; }

    [JsonIgnore]
    [ForeignKey(name: nameof(BuildingId))]
    public virtual BuildingEntity Building { get; set; }
}
