namespace PropertyRenting.Api.DTOs;

public class ExpenseDTO
{
    public Guid Id { get; set; }
    public string NameAR { get; set; }
    public string NameEN { get; set; }
    public Guid AccountId { get; set; }
}