namespace PropertyRenting.Api.Contracts.Requests;

public record BuildingBalanceRequest(Guid? BuildingId, DateTime? FromDate, DateTime? ToDate);
