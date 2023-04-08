using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PropertyRenting.Api.Models.Entities;

[Table("Building")]
public class BuildingEntity : BaseEntity
{
    public int SymbolNo { get; set; }
    public bool Status { get; set; }
    public string Name { get; set; }
    public int TypeId { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid DistrictId { get; set; }
    public string AddressAR { get; set; }
    public string AddressEN { get; set; }
    public string Location { get; set; }
    public string Latitude { get; set; }
    public string Longtude { get; set; }
    public int ConstructionStatusId { get; set; }
    public int EstablisYear { get; set; }
    public decimal TotalArea { get; set; }
    public decimal RentableArea { get; set; }
    public decimal YearRentAmount { get; set; }
    public decimal YearReRentAmount { get; set; }
    public int LevelNo { get; set; }
    public int UnitsNo { get; set; }
    public DateTime ReceiveDate { get; set; }
    public string Notes { get; set; }
    [JsonIgnore]
    [ForeignKey(name: nameof(EmployeeId))]
    public virtual EmployeeEntity Employee { get; set; }
    [JsonIgnore]
    [ForeignKey(name: nameof(DistrictId))]
    public virtual DistrictEntity District { get; set; }

    public virtual ICollection<UnitEntity> Units { get; set; }

    public virtual ICollection<BuildingContributerEntity> Contributers { get; set; }

}
