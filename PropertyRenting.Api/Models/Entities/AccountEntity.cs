using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace PropertyRenting.Api.Models.Entities;

[Table("Account")]
public class AccountEntity : BaseEntity
{
    public string Code { get; set; }
    public string NameAR { get; set; }
    public string NameEN { get; set; }
    public int AccountTypeId { get; set; }
    public Guid? ParentId { get; set; }
    public int Level { get; set; }
    [ForeignKey(nameof(ParentId))]
    [JsonIgnore]
    public virtual AccountEntity ParentAccount { get; set; }
    public virtual ICollection<AccountEntity> AccountChildren { get; set; }


    public virtual ICollection<AccountSetupEntity> AccruedRevenueAccounts { get; set; }
    public virtual ICollection<AccountSetupEntity> RevenueAccounts { get; set; }
    public virtual ICollection<AccountSetupEntity> AccruedExpenseAccounts { get; set; }
    public virtual ICollection<AccountSetupEntity> ExpenseAccounts { get; set; }
    public virtual ICollection<AccountSetupEntity> ContributerAccounts { get; set; }
    public virtual ICollection<AccountSetupEntity> RenterAccounts { get; set; }
    public virtual ICollection<AccountSetupEntity> OwnerAccounts { get; set; }
}
