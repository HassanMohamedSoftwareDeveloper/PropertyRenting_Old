namespace PropertyRenting.Api.Contracts.Requests;

public record GetBuildingRevenueExpenseRequest(Guid? BuildingId, DateTime? ToDate);
