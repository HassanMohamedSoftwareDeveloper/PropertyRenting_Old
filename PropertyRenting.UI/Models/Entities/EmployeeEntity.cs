using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyRenting.UI.Models.Entities;
[Table("Employee")]
public class EmployeeEntity : BaseEntity
{
    public string NameAR { get; set; }
    public string NameEN { get; set; }
}
