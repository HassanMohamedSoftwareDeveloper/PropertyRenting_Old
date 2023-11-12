BEGIN TRY
DECLARE @AccruedRevenueAccountId UNIQUEIDENTIFIER
       ,@AccruedExpenseAccountId UNIQUEIDENTIFIER;

SELECT TOP 1
    @AccruedRevenueAccountId = AccruedRevenueAccountId
   ,@AccruedExpenseAccountId = AccruedExpenseAccountId
FROM AccountSetup;

WITH BuidlingWithContributors
AS
(SELECT
        Contributer.Id AS ContributorId
       ,Contributer.NameAR AS Contributor
       ,BuildingContributer.BuildingId
       ,(BuildingContributer.[Percentage] / 100) AS [Percentage]
    FROM Contributer
    LEFT JOIN BuildingContributer
        ON Contributer.Id = BuildingContributer.ContributerId
    WHERE (@ContributorId IS NULL
    OR Contributer.Id = @ContributorId)),
Vouchers
AS
(SELECT
        BuidlingWithContributors.ContributorId
       ,BuidlingWithContributors.Contributor
       ,Voucher.VoucherDate
       ,(CASE
            WHEN Details.ContributerId IS NOT NULL THEN ISNULL(Details.DebitAmount, 0)
            ELSE (ISNULL(Details.DebitAmount, 0) * BuidlingWithContributors.Percentage)
        END) AS Debit
       ,(CASE
            WHEN Details.ContributerId IS NOT NULL THEN ISNULL(Details.CreditAmount, 0)
            ELSE (ISNULL(Details.CreditAmount, 0) * BuidlingWithContributors.Percentage)
        END) AS Credit

    FROM VoucherDetails Details
    JOIN Voucher
        ON Details.VoucherId = Voucher.Id
    JOIN BuidlingWithContributors
        ON (Details.BuildingId IS NOT NULL
        AND Details.BuildingId = BuidlingWithContributors.BuildingId)
        OR (Details.ContributerId IS NOT NULL
        AND Details.ContributerId = BuidlingWithContributors.ContributorId)
    WHERE Details.RenterId IS NULL
    AND Details.OwnerId IS NULL
    AND Details.AccountId != @AccruedRevenueAccountId
    AND Details.AccountId != @AccruedExpenseAccountId
    AND (@ToDate IS NULL
    OR CONVERT(DATE, Voucher.VoucherDate) <= @ToDate))

SELECT
    Vouchers.ContributorId
   ,Vouchers.Contributor
   ,SUM(CASE
        WHEN @FromDate IS NOT NULL AND
            VoucherDate < @FromDate THEN Debit - Credit
        ELSE 0
    END) AS 'BeginPeriodBalance'
   ,SUM(CASE
        WHEN @FromDate IS NOT NULL AND
            VoucherDate < @FromDate THEN 0
        ELSE Debit
    END) AS 'Debit'
   ,SUM(CASE
        WHEN @FromDate IS NOT NULL AND
            VoucherDate < @FromDate THEN 0
        ELSE Credit
    END) AS 'Credit'
   ,SUM(Debit - Credit) AS 'EndPeriodBalance'
FROM Vouchers
GROUP BY Vouchers.ContributorId
        ,Vouchers.Contributor


END TRY
BEGIN CATCH
END CATCH

