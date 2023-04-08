namespace PropertyRenting.Api.DTOs;
public class UnitDTO
{
    public Guid Id { get; set; }
    public string UnitNumber { get; set; }
    public string UnitName { get; set; }
    public int RoomsNumber { get; set; }
    public bool Status { get; set; }
    public int TypeId { get; set; }
    public Guid BuildingId { get; set; }
    public int Floor { get; set; }
    public bool RentStatus { get; set; }
    public DateTime ReceiveDate { get; set; }
    public decimal TotalArea { get; set; }
    public decimal RentableArea { get; set; }
    public decimal AnnualRentAmount { get; set; }
    public bool HasCentralAC { get; set; }
    public bool HasInternetService { get; set; }
    public Guid DistrictId { get; set; }
    public string AddressAR { get; set; }
    public string AddressEN { get; set; }
    public string Location { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string Notes { get; set; }
    public int PathsNumber { get; set; }
    public int HallNumber { get; set; }
    public int ACNumber { get; set; }
    public int KitchenNumber { get; set; }
    public string District { get; set; }
    public Guid CountryId { get; set; }
    public string Country { get; set; }
    public Guid CityId { get; set; }
    public string City { get; set; }

}