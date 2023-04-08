using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyRenting.Api.Models.Entities;

[Table("Nationality")]
public class NationalityEntity : BaseEntity
{
    public string NameAR { get; set; }
    public string NameEN { get; set; }


    public virtual ICollection<RenterEntity> Renters { get; set; }
}
