using System.ComponentModel.DataAnnotations;

namespace PropertyRenting.UI.Models.Entities;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; }
}
