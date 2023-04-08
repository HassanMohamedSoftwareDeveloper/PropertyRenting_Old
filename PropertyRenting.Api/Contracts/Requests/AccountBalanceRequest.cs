namespace PropertyRenting.Api.Contracts.Requests;

public record AccountBalanceRequest(Guid? AccountId, DateTime? FromDate, DateTime? ToDate);
