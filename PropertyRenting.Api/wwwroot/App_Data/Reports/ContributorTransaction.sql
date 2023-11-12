BEGIN TRY
DECLARE @AccruedRevenueAccountId UNIQUEIDENTIFIER
       ,@AccruedExpenseAccountId UNIQUEIDENTIFIER
SELECT TOP 1
    @AccruedRevenueAccountId = AccruedRevenueAccountId
   ,@AccruedExpenseAccountId = AccruedExpenseAccountId
FROM AccountSetup;


SELECT
    BuildingId
   ,([Percentage] / 100) [Percentage] INTO #TBuildingIds
FROM BuildingContributer
WHERE ContributerId = @ContributorId;



SELECT
    Voucher.AutoNumber
   ,Voucher.VoucherId
   ,Voucher.VoucherDate
   ,Voucher.ReferenceId
   ,Voucher.ReferenceType
   ,Voucher.ReferenceAutoNumber
   ,Voucher.ReferenceManualNumber
   ,#TBuildingIds.Percentage
   ,(CASE
        WHEN VoucherDetails.ContributerId IS NOT NULL THEN ISNULL(VoucherDetails.DebitAmount, 0)
        ELSE (ISNULL(VoucherDetails.DebitAmount, 0) * COALESCE(#TBuildingIds.Percentage, 0))
    END) AS DebitAmount
   ,(CASE
        WHEN VoucherDetails.ContributerId IS NOT NULL THEN ISNULL(VoucherDetails.CreditAmount, 0)
        ELSE (ISNULL(VoucherDetails.CreditAmount, 0) * COALESCE(#TBuildingIds.Percentage, 0))
    END) AS CreditAmount
   ,Voucher.Description INTO #TempData
FROM Voucher
JOIN VoucherDetails
    ON Voucher.Id = VoucherDetails.VoucherId
LEFT JOIN  #TBuildingIds
    ON VoucherDetails.BuildingId = #TBuildingIds.BuildingId
WHERE (VoucherDetails.ContributerId = @ContributorId
OR VoucherDetails.BuildingId IN (SELECT
        BuildingId
    FROM #TBuildingIds)
)
AND (@ToDate IS NULL
OR CONVERT(DATE, Voucher.VoucherDate) <= @ToDate)
AND VoucherDetails.RenterId IS NULL
AND VoucherDetails.OwnerId IS NULL
AND VoucherDetails.AccountId != @AccruedRevenueAccountId
AND VoucherDetails.AccountId != @AccruedExpenseAccountId



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
FROM #TempData
ORDER BY Type, VoucherDate

DROP TABLE #TempData;
DROP TABLE #TBuildingIds;
END TRY
BEGIN CATCH
DROP TABLE #TempData;
DROP TABLE #TBuildingIds;
END CATCH