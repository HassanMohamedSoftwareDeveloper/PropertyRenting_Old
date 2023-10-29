namespace PropertyRenting.Api.DTOs;

public class ReceiptVoucherDTO
{
    public Guid Id { get; set; }
    public int SanadTypeId { get; set; }
    public long AutoNumber { get; set; }
    public string SanadNumber { get; set; }
    public string From { get; set; }
    public DateTime SanadDate { get; set; }
    public Guid CashBankId { get; set; }
    public Guid? RenterId { get; set; }
    public Guid? OwnerId { get; set; }
    public Guid? ContributerId { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public int StateId { get; set; }
    public string CashBank { get; set; }
    public string Renter { get; set; }
    public string Owner { get; set; }
    public string Contributer { get; set; }
    public List<ReceiptVoucherDetailsDTO> SanadDetails { get; set; }
}
