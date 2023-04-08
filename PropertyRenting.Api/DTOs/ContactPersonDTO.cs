namespace PropertyRenting.Api.DTOs;

public class ContactPersonDTO
{
    public Guid Id { get; set; }
    public int Code { get; set; }
    public bool Status { get; set; }
    public string NameAR { get; set; }
    public string NameEN { get; set; }
    public string Phone1 { get; set; }
    public string Mobile1 { get; set; }
    public string Email { get; set; }
    public int IdentityTypeId { get; set; }
    public string IdentityNumber { get; set; }
    public string IdentityIssuePlace { get; set; }
    public DateTime? IdentityIssueDate { get; set; }
    public DateTime? IdentityExpiryDate { get; set; }
    public string Notes { get; set; }
    public Guid RenterId { get; set; }
}