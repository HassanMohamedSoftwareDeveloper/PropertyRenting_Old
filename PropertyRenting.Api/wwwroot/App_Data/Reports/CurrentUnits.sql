SELECT
    UnitNumber
   ,UnitName
   ,Building.Name AS 'BuildingName'
   ,Unit.AddressAR
   ,Unit.AddressEN
   ,District.NameAR AS 'District'
   ,City.NameAR AS 'City'
   ,Country.NameAR AS 'Country'
   ,Unit.AnnualRentAmount
   ,Unit.TotalArea
   ,Unit.RentableArea
FROM Unit
JOIN Building
    ON Unit.BuildingId = Building.Id
JOIN District
    ON Unit.DistrictId = District.Id
JOIN City
    ON District.CityId = City.Id
JOIN Country
    ON City.CountryId = Country.Id
ORDER BY BuildingName, Unit.CreatedOnUtc