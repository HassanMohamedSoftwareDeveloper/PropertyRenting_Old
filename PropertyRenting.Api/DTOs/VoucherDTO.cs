namespace PropertyRenting.Api.DTOs;

public class VoucherDTO
{
    public Guid Id { get; set; }
    public string VoucherId { get; set; }
    public long AutoNumber { get; set; }
    public DateTime VoucherDate { get; set; }
    public Guid? ReferenceId { get; set; }
    public long? ReferenceAutoNumber { get; set; }
    public string ReferenceManualNumber { get; set; }
    public string ReferenceType { get; set; }
    public string Description { get; set; }
    public int StateId { get; set; }
    public List<VoucherDetailsDTO> VoucherDetails { get; set; }
}
