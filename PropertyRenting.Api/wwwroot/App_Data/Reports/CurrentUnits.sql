SELECT
	UnitNumber
   ,UnitName
   ,Building.Name AS 'BuildingName'
   ,Unit.AddressAR
   ,Unit.AddressEN
   ,CONCAT(District.NameAR, ' - ', District.NameEN) AS 'District'
   ,CONCAT(City.NameAR, ' - ', City.NameEN) AS 'City'
   ,CONCAT(Country.NameAR, ' - ', Country.NameEN) AS 'Country'
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