namespace PropertyRenting.Api.DTOs;

public class BuildingContributerDTO
{
    public Guid Id { get; set; }
    public Guid ContributerId { get; set; }
    public string Contributer { get; set; }
    public decimal Percentage { get; set; }
}
