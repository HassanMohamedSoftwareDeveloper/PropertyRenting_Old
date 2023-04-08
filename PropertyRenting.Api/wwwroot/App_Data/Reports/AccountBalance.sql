SELECT
	Details.AccountId
   ,Acc.Code AS 'AccountCode'
   ,Acc.NameAR As 'AccountNameAR'
   ,Acc.NameEN As 'AccountNameEN'
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
JOIN Account Acc
	ON Details.AccountId = acc.Id

WHERE (@AccountId IS NULL
OR Details.AccountId = @AccountId)
AND (@ToDate IS NULL
OR CONVERT(DATE, Voucher.VoucherDate) <= @ToDate)

GROUP BY Details.AccountId
		,acc.Code
		,Acc.NameAR
		,Acc.NameEN