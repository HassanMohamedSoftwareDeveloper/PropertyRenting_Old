namespace PropertyRenting.Api.DTOs;

public class InstallmentsPerDateDTO
{
    public string DueDate { get; set; }
    public int Count { get; set; }
    public decimal Total { get; set; }
}
