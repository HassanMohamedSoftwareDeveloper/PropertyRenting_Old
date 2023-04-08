namespace PropertyRenting.Api.Contracts.Requests;

public record CashBankTransactionRequest(Guid CashBankId, DateTime? FromDate, DateTime? ToDate, string CashBankName);
