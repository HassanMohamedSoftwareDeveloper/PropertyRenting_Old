using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PropertyRenting.UI.Models.Entities;

[Table("ContactPerson")]
public class ContactPersonEntity : BaseEntity
{
    public int ContactTypeId { get; set; }
    public string NameAR { get; set; }
    public string NameEN { get; set; }
    public string Phone1 { get; set; }
    public string Phone2 { get; set; }
    public string Mobile1 { get; set; }
    public string Mobile2 { get; set; }
    public Guid DistrictId { get; set; }
    public string AddressAR { get; set; }
    public string AddressEN { get; set; }
    [JsonIgnore]
    [ForeignKey(name: nameof(DistrictId))]
    public virtual DistrictEntity District { get; set; }
}
