namespace PropertyRenting.Api.Contracts.Requests;

public record RenterDueInstallmentRequest(Guid? RenterId, Guid? UnitId, DateTime? ToDate);
