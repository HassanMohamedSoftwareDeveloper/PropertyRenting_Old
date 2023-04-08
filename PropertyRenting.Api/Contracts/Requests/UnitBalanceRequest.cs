namespace PropertyRenting.Api.Contracts.Requests;

public record UnitBalanceRequest(Guid? UnitId, DateTime? FromDate, DateTime? ToDate);
