namespace PropertyRenting.Api.Models.Entities;

public interface IAuditableEntity
{
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? UpdatedOnUtc { get; set; }
    public string CreatedBy { get; set; }
    public string ModifiedBy { get; set; }
}
