namespace PropertyRenting.Api.DTOs;

public class AccountSetupDTO
{
    public Guid Id { get; set; }
    public Guid AccruedRevenueAccountId { get; set; }
    public Guid RevenueAccountId { get; set; }
    public Guid AccruedExpenseAccountId { get; set; }
    public Guid ExpenseAccountId { get; set; }
    public Guid ContributerAccountId { get; set; }
    public Guid RenterAccountId { get; set; }
    public Guid OwnerAccountId { get; set; }
}
