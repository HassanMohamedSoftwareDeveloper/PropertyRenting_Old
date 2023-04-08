SELECT
	Contract.AutoNumber
   ,Contract.ContractNumber
   ,ConractTrans.Amount 
   ,ConractTrans.PaidAmount 
   ,ConractTrans.Balance 
   ,Addition.NameAR As 'TypeAR'
   ,Addition.NameEN As 'TypeEN'
   ,DueDate
   ,Renter.NameAR AS 'RenterAR'
   ,Renter.NameEN AS 'RenterEN'
   ,Building.Name AS 'BuildingName'
   ,Unit.UnitName
   ,Unit.UnitNumber
FROM Renter
JOIN RenterContract Contract
	ON Renter.Id = Contract.RenterId
JOIN RenterFinancialTransaction ConractTrans
	ON Contract.Id = ConractTrans.ContractId
JOIN Unit
	ON Contract.UnitId = Unit.Id
JOIN Building
	ON Unit.BuildingId = Building.Id
LEFT JOIN ContractAdditions Addition
	ON ConractTrans.ContractAdditionId = Addition.Id

WHERE (@RenterId IS NULL
OR Renter.Id = @RenterId)
AND (@UnitId IS NULL
OR Unit.Id = @UnitId)
AND (@ToDate IS NULL
OR CONVERT(DATE, ConractTrans.DueDate) <=  @ToDate)
AND (ConractTrans.IsPaid = 0)
AND (Contract.ContractState = 2)

ORDER BY Contract.AutoNumber,ConractTrans.DueDate