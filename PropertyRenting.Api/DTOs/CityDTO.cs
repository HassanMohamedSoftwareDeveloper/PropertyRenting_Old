namespace PropertyRenting.Api.DTOs;

public class CityDTO
{
    public Guid Id { get; set; }
    public string NameAR { get; set; }
    public string NameEN { get; set; }
    public Guid CountryId { get; set; }
    public string Country { get; set; }
}
