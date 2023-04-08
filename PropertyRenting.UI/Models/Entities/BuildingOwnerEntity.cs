using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PropertyRenting.UI.Models.Entities;

[Table("BuildingOwner")]
public class BuildingOwnerEntity : BaseEntity
{
    public Guid OwnerId { get; set; }
    public Guid BuildingId { get; set; }
    [Precision(2)]
    public decimal Percentage { get; set; }

    [JsonIgnore]
    [ForeignKey(name: nameof(OwnerId))]
    public virtual OwnerEntity Owner { get; set; }

    [JsonIgnore]
    [ForeignKey(name: nameof(BuildingId))]
    public virtual BuildingEntity Building { get; set; }
}
