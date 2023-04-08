namespace PropertyRenting.Api.DTOs;

public class BuildingDTO
{
    public Guid Id { get; set; }
    public int SymbolNo { get; set; }
    public bool Status { get; set; }
    public string Name { get; set; }
    public int TypeId { get; set; }
    public string Type { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid DistrictId { get; set; }
    public string AddressAR { get; set; }
    public string AddressEN { get; set; }
    public string Location { get; set; }
    public string Latitude { get; set; }
    public string Longtude { get; set; }
    public int ConstructionStatusId { get; set; }
    public string ConstructionStatus { get; set; }
    public int EstablisYear { get; set; }
    public decimal TotalArea { get; set; }
    public decimal RentableArea { get; set; }
    public decimal YearRentAmount { get; set; }
    public decimal YearReRentAmount { get; set; }
    public int LevelNo { get; set; }
    public int UnitsNo { get; set; }
    public DateTime ReceiveDate { get; set; }

    public string Employee { get; set; }
    public string District { get; set; }
    public Guid CityId { get; set; }
    public string City { get; set; }
    public Guid CountryId { get; set; }
    public string Country { get; set; }

    public string Notes { get; set; }

    public List<BuildingContributerDTO> Contributers { get; set; }
}
