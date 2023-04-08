
BEGIN TRY

DECLARE @AccruedRevenueAccountId UNIQUEIDENTIFIER
	   ,@AccruedExpenseAccountId UNIQUEIDENTIFIER
SELECT TOP 1
	@AccruedRevenueAccountId = AccruedRevenueAccountId
   ,@AccruedExpenseAccountId = AccruedExpenseAccountId
FROM AccountSetup





SELECT
	Details.BuildingId
   ,Building.Name AS 'BuildingName'
   ,SUM(CASE WHEN Account.AccountTypeId=4 THEN(ISNULL(Details.CreditAmount, 0)-ISNULL(Details.DebitAmount, 0)) ELSE 0 END) AS 'TotalRevenue'
   ,SUM(CASE WHEN Account.AccountTypeId=3 THEN(ISNULL(Details.DebitAmount, 0)-ISNULL(Details.CreditAmount, 0)) ELSE 0 END) AS 'TotalExpense'

FROM VoucherDetails Details
JOIN Voucher Voucher
	ON Details.VoucherId = Voucher.Id
JOIN Building
	ON Details.BuildingId = Building.Id
JOIN Account
	ON Details.AccountId = Account.Id
WHERE (@BuildingId IS NULL
OR Details.BuildingId = @BuildingId)
AND (@ToDate IS NULL
OR CONVERT(DATE, Voucher.VoucherDate) <= @ToDate)
AND Details.RenterId IS NULL
AND Details.OwnerId IS NULL
AND Details.AccountId !=@AccruedRevenueAccountId AND Details.AccountId!= @AccruedExpenseAccountId
AND Account.AccountTypeId IN(3,4)

GROUP BY Details.BuildingId
		,Building.Name


END TRY
BEGIN CATCH
END CATCH