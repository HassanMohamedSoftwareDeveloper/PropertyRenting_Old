using System.ComponentModel.DataAnnotations;

namespace PropertyRenting.Api.Models.Entities;

public abstract class BaseEntity : IAuditableEntity
{
    [Key]
    public Guid Id { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? UpdatedOnUtc { get; set; }
    public string CreatedBy { get; set; }
    public string ModifiedBy { get; set; }
}
