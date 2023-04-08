namespace PropertyRenting.Api.DTOs.Reports;

public class AvailableUnitDTO
{
    public string UnitNumber { get; set; }
    public string UnitName { get; set; }
    public string BuildingName { get; set; }
    public string AddressAR { get; set; }
    public string AddressEN { get; set; }
    public string District { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public decimal AnnualRentAmount { get; set; }
    public decimal TotalArea { get; set; }
    public decimal RentableArea { get; set; }
}

