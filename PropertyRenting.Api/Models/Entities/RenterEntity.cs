using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PropertyRenting.Api.Models.Entities;

[Table("Renter")]
public class RenterEntity : BaseEntity
{
    public bool Status { get; set; }
    public int TypeId { get; set; }
    public string NameAR { get; set; }
    public string NameEN { get; set; }
    public Guid NationalityId { get; set; }
    public int IdentityTypeId { get; set; }
    public string IdentityNumber { get; set; }
    public string IdentityIssuePlace { get; set; }
    public DateTime IdentityIssueDate { get; set; }
    public DateTime IdentityExpiryDate { get; set; }

    public Guid CityId { get; set; }
    public string RegionCode { get; set; }
    public string PostalCode { get; set; }
    public string Email { get; set; }
    public string Phone1 { get; set; }
    public string Phone2 { get; set; }
    public string Mobile1 { get; set; }
    public string Mobile2 { get; set; }
    public string Fax { get; set; }

    public string GuarantorName { get; set; }
    public string GuarantorPhone { get; set; }
    public string GuarantorAddress { get; set; }

    public int GenderTypeId { get; set; }

    public bool IsBlackListed { get; set; }
    public string Notes { get; set; }

    [JsonIgnore]
    [ForeignKey(name: nameof(CityId))]
    public virtual CityEntity City { get; set; }
    [JsonIgnore]
    [ForeignKey(name: nameof(NationalityId))]
    public virtual NationalityEntity Nationality { get; set; }
    public virtual ICollection<ContactPersonEntity> ContactPersons { get; set; }
}
