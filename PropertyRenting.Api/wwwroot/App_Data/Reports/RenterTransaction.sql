BEGIN TRY
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
   ,Unit.UnitName
   ,Unit.UnitNumber
   ,Building.Name INTO #TempData
FROM Voucher
JOIN VoucherDetails
	ON Voucher.Id = VoucherDetails.VoucherId
LEFT JOIN Unit
	ON Unit.Id = VoucherDetails.UnitId
LEFT JOIN Building
	ON Building.Id = Unit.BuildingId
WHERE (VoucherDetails.RenterId = @RenterId)
AND (@ToDate IS NULL
OR CONVERT(DATE, Voucher.VoucherDate) <= @ToDate)


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
   ,NULL AS 'UnitName'
   ,NULL AS 'UnitNumber'
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
   ,UnitName
   ,UnitNumber
   ,Name AS 'BuildingName'
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
		,UnitName
		,UnitNumber
		,Name
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
   ,NULL AS 'UnitName'
   ,NULL AS 'UnitNumber'
   ,NULL AS 'BuildingName'
FROM #TempData
ORDER BY Type, VoucherDate

DROP TABLE #TempData;
END TRY
BEGIN CATCH
DROP TABLE #TempData;
END CATCH