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
   ,SUM(CASE
		WHEN @FromDate IS NOT NULL AND
			Voucher.VoucherDate < @FromDate THEN ISNULL(DebitAmount, 0) - ISNULL(CreditAmount, 0)
		ELSE 0
	END) AS 'BeginPeriodBalance'
   ,SUM(CASE
		WHEN @FromDate IS NOT NULL AND
			Voucher.VoucherDate < @FromDate THEN 0
		ELSE ISNULL(DebitAmount, 0)
	END) AS 'Debit'
   ,SUM(CASE
		WHEN @FromDate IS NOT NULL AND
			Voucher.VoucherDate < @FromDate THEN 0
		ELSE ISNULL(CreditAmount, 0)
	END) AS 'Credit'
   ,SUM(ISNULL(DebitAmount, 0) - ISNULL(CreditAmount, 0)) AS 'EndPeriodBalance'

FROM VoucherDetails Details
JOIN Voucher Voucher
	ON Details.VoucherId = Voucher.Id
JOIN Building
	ON Details.BuildingId = Building.Id

WHERE (@BuildingId IS NULL
OR Details.BuildingId = @BuildingId)
AND (@ToDate IS NULL
OR CONVERT(DATE, Voucher.VoucherDate) <= @ToDate)
AND Details.RenterId IS NULL
AND Details.OwnerId IS NULL
AND Details.AccountId !=@AccruedRevenueAccountId AND Details.AccountId!= @AccruedExpenseAccountId

GROUP BY Details.BuildingId
		,Building.Name


END TRY
BEGIN CATCH
END CATCH