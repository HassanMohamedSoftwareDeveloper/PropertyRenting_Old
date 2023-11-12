namespace PropertyRenting.Api.DTOs.Reports;

public class ContributorTransactionDTO
{
    public int Type { get; set; }
    public decimal? DebitAmount { get; set; }
    public decimal? CreditAmount { get; set; }
    public long? AutoNumber { get; set; }
    public string VoucherId { get; set; }
    public DateTime? VoucherDate { get; set; }
    public Guid? ReferenceId { get; set; }
    public string ReferenceType { get; set; }
    public long? ReferenceAutoNumber { get; set; }
    public string ReferenceManualNumber { get; set; }
    public string Description { get; set; }
}
