SELECT
	Renter.NameAR AS 'RenterNameAR'
   ,Renter.NameEN AS 'RenterNameEN'
   ,Renter.Mobile1 AS 'MobileNumber'
   ,Building.Name AS 'BuildingName'
   ,Unit.UnitName
   ,Unit.UnitNumber
FROM Renter
JOIN RenterContract Contract
	ON Renter.Id = Contract.RenterId
JOIN Unit
	ON Contract.UnitId = Unit.Id
JOIN Building
	ON Unit.BuildingId = Building.Id

	WHERE Contract.ContractState=2