namespace PropertyRenting.Api.Contracts.Requests;

public record BuildingTransactionRequest(Guid BuildingId, DateTime? FromDate, DateTime? ToDate, string BuildingName);
