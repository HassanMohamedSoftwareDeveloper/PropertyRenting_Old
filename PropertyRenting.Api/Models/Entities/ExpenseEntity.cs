using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyRenting.Api.Models.Entities;

[Table("Expense")]
public class ExpenseEntity : BaseEntity
{
    public string NameAR { get; set; }
    public string NameEN { get; set; }
    public Guid AccountId { get; set; }
}