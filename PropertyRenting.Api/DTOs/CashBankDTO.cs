namespace PropertyRenting.Api.DTOs;

public class CashBankDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int TypeId { get; set; }
    public Guid AccountId { get; set; }
}
