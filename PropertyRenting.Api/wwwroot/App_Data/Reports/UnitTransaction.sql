﻿BEGIN TRY

DECLARE @AccruedRevenueAccountId UNIQUEIDENTIFIER
	   ,@AccruedExpenseAccountId UNIQUEIDENTIFIER
SELECT TOP 1
	@AccruedRevenueAccountId = AccruedRevenueAccountId
   ,@AccruedExpenseAccountId = AccruedExpenseAccountId
FROM AccountSetup



SELECT
	Voucher.AutoNumber
   ,Voucher.VoucherId
   ,Voucher.VoucherDate
   ,Voucher.ReferenceId
   ,Voucher.ReferenceType
   ,Voucher.ReferenceAutoNumber
   ,Voucher.ReferenceManualNumber
   ,VoucherDetails.DebitAmount
   ,VoucherDetails.CreditAmount
   ,Voucher.Description
   ,Building.Name AS 'BuildingName' INTO #TempData
FROM Voucher
JOIN VoucherDetails
	ON Voucher.Id = VoucherDetails.VoucherId
JOIN Building ON Building.Id = VoucherDetails.BuildingId

WHERE (VoucherDetails.UnitId = @UnitId)
AND (@ToDate IS NULL
OR CONVERT(DATE, Voucher.VoucherDate) <= @ToDate)
AND VoucherDetails.RenterId IS NULL AND VoucherDetails.OwnerId IS NULL
AND VoucherDetails.AccountId !=@AccruedRevenueAccountId AND VoucherDetails.AccountId!= @AccruedExpenseAccountId

SELECT
	1 AS 'Type'
   ,NULL AS 'AutoNumber'
   ,NULL AS 'VoucherId'
   ,NULL AS 'VoucherDate'
   ,NULL AS 'ReferenceId'
   ,NULL AS 'ReferenceType'
   ,NULL AS 'ReferenceAutoNumber'
   ,NULL AS 'ReferenceManualNumber'
   ,SUM(ISNULL(DebitAmount, 0)) 'DebitAmount'
   ,SUM(ISNULL(CreditAmount, 0)) 'CreditAmount'
   ,NULL AS 'Description'
   ,NULL AS 'BuildingName'
FROM #TempData
WHERE @FromDate IS NOT NULL
AND CONVERT(DATE, VoucherDate) < @FromDate

UNION ALL
SELECT
	2 AS 'Type'
   ,AutoNumber
   ,VoucherId
   ,VoucherDate
   ,ReferenceId
   ,ReferenceType
   ,ReferenceAutoNumber
   ,ReferenceManualNumber
   ,SUM(ISNULL(DebitAmount, 0)) AS 'DebitAmount'
   ,SUM(ISNULL(CreditAmount, 0)) AS 'CreditAmount'
   ,Description
   ,BuildingName
FROM #TempData
WHERE (@FromDate IS NULL
OR CONVERT(DATE, VoucherDate) >= @FromDate)
GROUP BY AutoNumber
		,VoucherId
		,VoucherDate
		,ReferenceId
		,ReferenceType
		,ReferenceAutoNumber
		,ReferenceManualNumber
		,Description
		,BuildingName
UNION ALL
SELECT
	3 AS 'Type'
   ,NULL AS 'AutoNumber'
   ,NULL AS 'VoucherId'
   ,NULL AS 'VoucherDate'
   ,NULL AS 'ReferenceId'
   ,NULL AS 'ReferenceType'
   ,NULL AS 'ReferenceAutoNumber'
   ,NULL AS 'ReferenceManualNumber'
   ,SUM(ISNULL(DebitAmount, 0)) 'DebitAmount'
   ,SUM(ISNULL(CreditAmount, 0)) 'CreditAmount'
   ,NULL AS 'Description'
   ,NULL AS 'BuildingName'
FROM #TempData
ORDER BY Type, VoucherDate

DROP TABLE #TempData;
END TRY
BEGIN CATCH
DROP TABLE #TempData;
END CATCH