SELECT
	Details.CashBankId
   ,CashBank.Name AS 'CashBankName'
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
JOIN CashBank
	ON Details.CashBankId = CashBank.Id


WHERE (@CashBankId IS NULL
OR Details.UnitId = @CashBankId)
AND (@ToDate IS NULL
OR CONVERT(DATE, Voucher.VoucherDate) <= @ToDate)

GROUP BY Details.CashBankId
		,CashBank.Name