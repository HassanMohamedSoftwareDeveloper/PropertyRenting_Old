SELECT
	Contract.AutoNumber
   ,Contract.ContractNumber
   ,ConractTrans.Amount 
   ,ConractTrans.PaidAmount 
   ,ConractTrans.Balance 
   ,DueDate
   ,Owner.NameAR AS 'OwnerAR'
   ,Owner.NameEN AS 'OwnerEN'
   ,Building.Name AS 'BuildingName'
FROM Owner
JOIN OwnerContract Contract
	ON Owner.Id = Contract.OwnerId
JOIN OwnerFinancialTransaction ConractTrans
	ON Contract.Id = ConractTrans.ContractId
JOIN Building
	ON Contract.BuildingId = Building.Id

WHERE (@OwnerId IS NULL
OR Owner.Id = @OwnerId)
AND (@BuildingId IS NULL
OR Building.Id = @BuildingId)
AND (@ToDate IS NULL
OR CONVERT(DATE, ConractTrans.DueDate) <=  @ToDate)
AND (ConractTrans.IsPaid = 0)
AND (Contract.ContractState = 2)

ORDER BY Contract.AutoNumber,ConractTrans.DueDate