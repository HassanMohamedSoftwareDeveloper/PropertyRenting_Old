namespace PropertyRenting.Api.Contracts.Requests;

public record AccountTransactionRequest(Guid AccountId, DateTime? FromDate, DateTime? ToDate, string AccountName);
