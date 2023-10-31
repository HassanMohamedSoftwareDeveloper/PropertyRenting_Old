namespace PropertyRenting.Api.DTOs;

public class BuildingUnitsCountDTO
{
    public string Building { get; set; }
    public int Total { get; set; }
    public int Rented { get; set; }
    public int Available { get; set; }
}
