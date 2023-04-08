using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyRenting.Api.Models.Entities;

[Table("ActionLog", Schema = "Log")]
public class ActionLogEntity
{
    [Key]
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public DateTime ActionDate { get; set; }
    public string Data { get; set; }
    public string Action { get; set; }
    public string Type { get; set; }
}
