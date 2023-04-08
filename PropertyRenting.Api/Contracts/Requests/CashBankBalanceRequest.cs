namespace PropertyRenting.Api.Contracts.Requests;

public record CashBankBalanceRequest(Guid? CashBankId, DateTime? FromDate, DateTime? ToDate);
