namespace PropertyRenting.Api.DTOs;

public class FlatAccountDto
{
    public Guid Id { get; set; }
    public string NameAR { get; set; }
    public string NameEN { get; set; }
    public int AccountTypeId { get; set; }
    public string Code { get; set; }
    public Guid? ParentId { get; set; }
    public int Level { get; set; }
    public bool HasChildren { get; set; }
}
