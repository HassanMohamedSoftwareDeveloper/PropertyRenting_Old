namespace PropertyRenting.Api.Contracts.Requests;

public record OwnerDueInstallmentRequest(Guid? OwnerId, Guid? BuildingId, DateTime? ToDate);
