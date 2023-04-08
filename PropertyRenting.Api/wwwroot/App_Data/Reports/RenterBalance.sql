SELECT
	Details.RenterId
   ,Renter.NameAR As 'RenterNameAR'
   ,Renter.NameEN As 'RenterNameEN'
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
JOIN Renter 
	ON Details.RenterId = Renter.Id

WHERE (@RenterId IS NULL
OR Details.RenterId = @RenterId)
AND (@ToDate IS NULL
OR CONVERT(DATE, Voucher.VoucherDate) <= @ToDate)

GROUP BY Details.RenterId
		,Renter.NameAR
		,Renter.NameEN