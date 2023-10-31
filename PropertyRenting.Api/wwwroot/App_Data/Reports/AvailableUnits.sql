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
LEFT JOIN RenterContract
    ON Unit.Id = RenterContract.UnitId
WHERE RenterContract.Id IS NULL
OR RenterContract.ContractState <> 2
OR (RenterContract.ContractState = 2
AND CONVERT(DATE, RenterContract.ContractEndDate) < CONVERT(DATE, GETDATE()))
ORDER BY BuildingName, Unit.CreatedOnUtc
