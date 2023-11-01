SELECT
    Contract.ContractStartDate
   ,Contract.ContractEndDate
   ,ConractTrans.Amount 
   ,ConractTrans.PaidAmount 
   ,ConractTrans.Balance 
   ,Addition.NameAR As 'TypeAR'
   ,DueDate
   ,DATEDIFF(DAY,GETDATE(),ConractTrans.DueDate) RemainingDays
   ,Renter.NameAR AS 'RenterAR'
   ,Renter.Mobile1 MobileNumber
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